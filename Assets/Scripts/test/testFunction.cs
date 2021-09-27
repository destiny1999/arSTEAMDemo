using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testFunction : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private WebCamTexture frontCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    // Start is called before the first frame update
    void Start()
    {

        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {

            //            if (!devices [i].isFrontFacing) {    //開啟後鏡頭
            if (devices[i].isFrontFacing)
            {    //開啟前鏡頭
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }
        if (backCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }
        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
    }
    private void Update()
    {
        if (!camAvailable)
            return;

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        //        background.rectTransform.localScale = new Vector3 (1f, scaleY, 1f);    //非鏡像
        background.rectTransform.localScale = new Vector3(-1f, scaleY, 1f);    //鏡像

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
