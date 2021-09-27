using OpenCvSharp;
using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System;
using MyBox;
using UnityEngine.SceneManagement;

public class ColoringScript : MonoBehaviour
{

    //public MeshRenderer target1;
    //public SkinnedMeshRenderer target2;
    public GameObject canvas, ShowObject;
    public RawImage viewL, viewR;
    UnityEngine.Rect capRect;
    Texture2D capTexture;
    Texture2D colTexture;
    Texture2D binTexture;
    Mat bgr, bin;
    [SerializeField] ARTrackedImageManager m_TrackedImageManager;
    [SerializeField] List<GameObject> targets = new List<GameObject>();
    Dictionary<string, GameObject> targetsDic = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> targetMDic = new Dictionary<string, GameObject>();
    //int index = -1;
    [SerializeField] bool skinned = false;
    [SerializeField] bool mesh = false;
    [SerializeField] bool mutipleMaterial = false;
    [SerializeField] [ConditionalField("mutipleMaterial")] int targetIndex = 0;
    static bool coloring = false;
    [SerializeField] [ConditionalField("skinned")] SkinnedMeshRenderer skinnedTarget = null;
    [SerializeField] [ConditionalField("mesh")] MeshRenderer meshTarget = null;

    [SerializeField] GameObject BT_Confirm = null;
    [SerializeField] GameObject BT_ReCapture = null;
    [SerializeField] GameObject BT_GoToCatch = null;

    [SerializeField] SkinnedMeshRenderer targetModelSkinnedMesh = null;
    [SerializeField] GameObject Panel = null;

    int type = -1;
    public static ColoringScript Instance;
    enum TargetType
    {
        normal,
        skinned
    }

    private void Awake()
    {
        Instance = this;


        if(skinned && mesh)
        {
            Debug.LogError("can't chose all mesh and skinned");
        }
        else
        {
            type = mesh ? 0 : 1;
        }
    }

    void Start()
    {
        int w = Screen.width;
        int h = Screen.height;

        int sx = (int)(w * 0.2);
        int sy = (int)(h * 0.3);
        w = (int)(w * 0.6);
        h = (int)(h * 0.4);

        capRect = new UnityEngine.Rect(sx, sy, w, h);

        capTexture = new Texture2D(w, h, TextureFormat.RGB24, false);

    }


    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        foreach (var updatedImage in eventArgs.updated)
        {

            if( updatedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {

                string targetName = updatedImage.referenceImage.name;
                Vector3 pos = updatedImage.transform.position;
                ShowObject.transform.position = pos;
                ShowObject.SetActive(true);

                
            }
            
        }
    }
    IEnumerator ImageProcessing()
    {
        coloring = true;
        canvas.SetActive(false);
        ShowObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        CreateImage();

        Point[] corners;
        FindRect(out corners);
        TransformImage(corners);
        ShowImage();

        bgr.Release();
        bin.Release();

        canvas.SetActive(true);
        ShowObject.SetActive(true);
        coloring = false;
        ShowObject.SetActive(true);

        BT_Confirm.SetActive(true);
        BT_ReCapture.SetActive(true);

    }
    void TransformImage(Point[] corners)
    {
        if (corners == null) return;

        SortCorners(corners);

        Point2f[] input = { corners[0], corners[1], corners[2], corners[3] };

        Point2f[] square = {new Point2f(0,0), new Point2f(0,255),
                            new Point2f(255,255), new Point2f(255,0)};

        Mat transform = Cv2.GetPerspectiveTransform(input, square);

        Cv2.WarpPerspective(bgr, bgr, transform, new Size(256, 256));

        int s = (int)(256 * 0.05);
        int w = (int)(256 * 0.9);
        OpenCvSharp.Rect innerRect = new OpenCvSharp.Rect(s, s, w, w);

        bgr = bgr[innerRect];

    }
    void SortCorners(Point[] corners)
    {
        
        System.Array.Sort(corners, (a, b) => a.X.CompareTo(b.X));
        if(corners[0].Y > corners[1].Y)
        {
            
            OpenCvSharp.Demo.ArrayUtilities.Swap(corners, 0, 1);
        }
        if(corners[3].Y > corners[2].Y)
        {
            OpenCvSharp.Demo.ArrayUtilities.Swap(corners, 2, 3);

        }
    }
    void FindRect(out Point[] corners)
    {
        corners = null;

        Point[][] contours;
        HierarchyIndex[] h;

        bin.FindContours(out contours, out h, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

        double maxArea = 0;
        for(int i = 0; i<contours.Length; i++)
        {
            double length = Cv2.ArcLength(contours[i], true);

            Point[] tmp = Cv2.ApproxPolyDP(contours[i], length * 0.01f, true);


            double area = Cv2.ContourArea(contours[i]);
            if (tmp.Length == 4 && area > maxArea)
            {
                maxArea = area;
                corners = tmp;
            }
        }
        /*
        if(corners != null)
        {
            bgr.DrawContours(new Point[][] { corners }, 0, Scalar.Red, 5);
            for(int i = 0; i<corners.Length; i++)
            {
                bgr.Circle(corners[i], 20, Scalar.Blue, 5);
            }
        }*/

    }
    void CreateImage()
    {
        capTexture.ReadPixels(capRect, 0, 0);
        capTexture.Apply();

        bgr = OpenCvSharp.Unity.TextureToMat(capTexture);

        bin = bgr.CvtColor(ColorConversionCodes.BGR2GRAY);

        bin = bin.Threshold(100, 255, ThresholdTypes.Otsu);
        Cv2.BitwiseNot(bin, bin);
    }
    void ShowImage()
    {
        if(colTexture != null) { DestroyImmediate(colTexture); }
        if(binTexture != null) { DestroyImmediate(binTexture); }

        colTexture = OpenCvSharp.Unity.MatToTexture(bgr);
        binTexture = OpenCvSharp.Unity.MatToTexture(bin);

        viewL.texture = colTexture;
        viewR.texture = binTexture;

        if(type == 0)
        {
            meshTarget.material.mainTexture = colTexture;
        }
        else if(!mutipleMaterial)
        {
            skinnedTarget.materials[0].mainTexture = colTexture;
        }
        else
        {
            skinnedTarget.materials[targetIndex].SetTexture("_MainTex", colTexture);
            skinnedTarget.materials[targetIndex].SetTexture("_ShadeTexture", colTexture);
        }

        canvas.SetActive(true);
    }
    public void StartCV(GameObject BT_Capture)
    {
        StartCoroutine(ImageProcessing());
        BT_Capture.SetActive(false);
        Panel.SetActive(false);

    }

    public void ClickGoToCatch()
    {
        WriteTexture();

        PlayerPrefs.SetInt("captureScene", 1);
        ChangeSceneManager.nextViewName = "locationBasic";
        SceneManager.LoadScene("LoadScene");

    }
    public void ClickConfirm(GameObject Panel)
    {
        BT_Confirm.SetActive(false);
        BT_ReCapture.SetActive(false);
        BT_GoToCatch.SetActive(true);
        Panel.SetActive(false);
        
    }
    public void ClickReCapture(GameObject BT_Capture)
    {
        ShowObject.SetActive(false);
        BT_Confirm.SetActive(false);
        BT_ReCapture.SetActive(false);
        BT_Capture.SetActive(true);
        Panel.SetActive(true);
    }
    void WriteTexture()
    {
        PlayerPrefs.SetInt("w", colTexture.width);
        PlayerPrefs.SetInt("h", colTexture.height);
        print("w = " + colTexture.width);
        print("h = " + colTexture.height);
        byte[] textureByte = colTexture.EncodeToPNG();
        string base64Texture = System.Convert.ToBase64String(textureByte);
        PlayerPrefs.SetString("colored", base64Texture);
        PlayerPrefs.Save();
    }
    public Texture2D GetCapctureTexture()
    {
        return colTexture;
    }
}





