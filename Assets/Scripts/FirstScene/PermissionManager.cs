using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermissionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if PLATFORM_ANDROID

        StartCoroutine(RequestAndroidPermission());

        #endif

        #if UNITY_IOS
            ShowIOSRequestView();
            //StartCoroutine(RequestIOSPermission());

        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowIOSRequestView()
    {
        Debug.LogWarning("Didn't Set IOS request View");
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
        request = false;
        while (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            if (!request)
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }
            request = true;
            yield return 1;
        }
    }
}
