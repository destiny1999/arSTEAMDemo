using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
#if USE_ARFOUNDATION
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
#endif

public class ARFoundationColorMapping : MonoBehaviour
{
#if USE_ARFOUNDATION
    public ARTrackedImageManager imageManager;
#endif

    public GameObject arPrefabs;

    public int realWidth;
    public int realHeight;

    private GameObject arContents;
    private Renderer drawObj;

    private GameObject cube;

    private bool isStart = false;
    private bool isEnd = false;

    public Text timeText;
    private float time;

    private void Update()
    {
        if (isStart)
        {
            time += Time.deltaTime;
        }

        if (isEnd)
        {
            timeText.text = (time * 1000).ToString() + "ms";

            time = 0.0f;
            isEnd = false;
        }
    }

#if USE_ARFOUNDATION
    void Start()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {


        ARTrackedImage trackedImage = null;

        for (int i = 0; i < eventArgs.added.Count; i++)
        {
            trackedImage = eventArgs.added[i];

            string imgName = trackedImage.referenceImage.name;

            if (imgName == arPrefabs.name)
            {
                arContents = Instantiate(arPrefabs, trackedImage.transform);
                Debug.Log(trackedImage.transform.localPosition.x);
                Debug.Log(trackedImage.transform.localPosition.y);
                cube = CreateCubeForARFoundationTarget(arContents.gameObject, trackedImage.size.x, trackedImage.size.y);
            }
        }

        for (int i = 0; i < eventArgs.updated.Count; i++)
        {
            trackedImage = eventArgs.updated[i];

            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                arContents.SetActive(true);
            }
            else
            {
                arContents.SetActive(false);
            }
        }

        for (int i = 0; i < eventArgs.removed.Count; i++)
        {
            arContents.SetActive(false);
        }
    }
#endif

    public void Play()
    {
        isStart = true;

        float[] srcValue = AirarManager.Instance.CalculateMarkerImageVertex(cube);

        Texture2D screenShotTex = ScreenShot.GetScreenShot(arContents);

        AirarManager.Instance.ProcessColoredMapTexture(screenShotTex, srcValue, realWidth, realHeight, (resultTex) =>
        {
            //drawObj = GameObject.FindGameObjectWithTag("coloring");
            drawObj = arContents.GetComponentsInChildren<Renderer>().First((item) => item.CompareTag("coloring"));
            drawObj.GetComponent<Renderer>().material.mainTexture = resultTex;

            isStart = false;
            isEnd = true;
            WriteTexture(resultTex);
        });
    }
    void WriteTexture(Texture2D targetTexture)
    {
        PlayerPrefs.SetInt("w", targetTexture.width);
        PlayerPrefs.SetInt("h", targetTexture.height);

        byte[] textureByte = targetTexture.EncodeToPNG();
        string base64Texture = System.Convert.ToBase64String(textureByte);
        PlayerPrefs.SetString("colored", base64Texture);
        PlayerPrefs.SetInt("colorOK", 1);
        PlayerPrefs.Save();
    }
    /// <summary>
    /// Create a full size cube on the ARFoundation marker image
    /// </summary>
    /// <param name="targetWidth">marker image width</param>
    /// <param name="targetHeight">marker image height</param>
    public GameObject CreateCubeForARFoundationTarget(GameObject parentObj, float targetWidth, float targetHeight)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = AirarManager.Instance.transparentMat;
        //cube.GetComponent<Renderer>().material.color = Color.green;
        cube.transform.SetParent(parentObj.transform);
        cube.transform.localPosition = Vector3.zero;
        cube.transform.localRotation = Quaternion.Euler(Vector3.zero);
        cube.transform.localScale = new Vector3(targetWidth, 0.001f, targetHeight);

        return cube;
    }
}
