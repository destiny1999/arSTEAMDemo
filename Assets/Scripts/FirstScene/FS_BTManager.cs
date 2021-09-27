using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif
#if UNITY_IOS
using UnityEngine.iOS;
#endif
public class FS_BTManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool reset = false;
    void Start()
    {

        #if PLATFORM_ANDROID

            StartCoroutine(RequestAndroidPermission());

        #endif

        #if UNITY_IOS
            
            StartCoroutine(RequestIOSPermission());

        #endif
        if (reset)
        {
            ResetData();
        }
        DealWithSaveData();
        DealWithButton();
    }
    IEnumerator RequestAndroidPermission()
    {
        bool request = false;
        while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            if (!request)
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
            request = true;
            yield return 1;
        }
        request = false;
        while (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            if (!request)
            {
                Permission.RequestUserPermission(Permission.Camera);
            }
            request = true;
            yield return 1;
        }
    }

    IEnumerator RequestIOSPermission()
    {



        bool request = false;
        
        request = false;
        while (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            if (!request)
            {
                Application.RequestUserAuthorization(UserAuthorization.WebCam);
            }
            request = true;
            yield return 1;
        }
    }


    public void ClickChangeViewButton(string _nextViewName)
    {
        ChangeSceneManager.SetNextViewName(_nextViewName);
        SceneManager.LoadScene("LoadScene");
    }
    void ResetData()
    {
        PlayerPrefs.SetInt("loginTimes", 0);
    }
    void DealWithSaveData()
    {
        //print(PlayerPrefs.GetInt("loginTimes"));
        if(PlayerPrefs.GetInt("loginTimes") == 0)
        {
            PlayerPrefs.SetInt("loginTimes", 1);
            PlayerPrefs.SetInt("captureScene", 0);
            PlayerPrefs.SetString("colored", "");
        }
    }
    void DealWithButton()
    {
        if(PlayerPrefs.GetInt("captureScene") == 1)
        {
            GameObject.Find("BT_GPS").GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
    }
}
