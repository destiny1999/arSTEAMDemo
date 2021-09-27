using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class GPSController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float targetLatitude = 0;
    [SerializeField] float targetLongitude = 0;
    float selfLatitude = 0;
    float selfLongitude = 0;
    bool gpsOpen = false;
    [SerializeField][Tooltip("After N seconds to get the gps data")] float delayTimeToGetData = 0;
    float delayTime = 0;
    [SerializeField][Tooltip("The object which will guide you to target")] GameObject GuideObject = null;
    GameObject target = null;

    float offsetLatitude = 0;
    float offsetLongitude = 0;
    [SerializeField][Tooltip("The target which will be seek")] GameObject targetObject = null;
    [SerializeField] TMP_Text debugInfo = null;

    private const double EARTH_RADIUS = 6378137;

    IEnumerator Start()
    {
        //debugInfo.text = "t0";
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            debugInfo.text = "not use location service";
            yield break;
        }
        

        //debugInfo.text = "t1";
        // Start service before querying location
        Input.location.Start();
        //debugInfo.text = "t2";
        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        //debugInfo.text = "t3";
        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            //print("Timed out");
            yield break;
        }
        //debugInfo.text = "t4";
        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            //print("Unable to determine device location");
            //debugInfo.text = "t5";
            yield break;
        }
        else
        {
            //debugInfo.text = "t6";
            // Access granted and location value could be retrieved
            //print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            selfLatitude = Input.location.lastData.latitude;
            selfLongitude = Input.location.lastData.longitude;

            offsetLatitude = selfLatitude;
            offsetLongitude = selfLongitude;
            SetTargetPosition();
            gpsOpen = true;
        }
        //debugInfo.text = "t7";
        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //GuideObject.transform.LookAt(target.transform);
        if (gpsOpen)
        {
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                gpsOpen = false;
                //print("Unable to determine device location");
                return;
            }
            GuideObject.transform.LookAt(target.transform);
            delayTime -= Time.deltaTime * 1;
            if (delayTime <= 0)
            {
                selfLatitude = Input.location.lastData.latitude;
                selfLongitude = Input.location.lastData.longitude;
                delayTime = delayTimeToGetData;
            }
            debugInfo.text = "self pos = " + Camera.main.transform.position;
            debugInfo.text += "\r\n target pos = " + target.transform.position;
        }
    }
    void SetTargetPosition()
    {
        //print("Set target pos");
        float targetAdjustLatitude = targetLatitude - offsetLatitude;
        float targetAdjustLongitude = targetLongitude - offsetLongitude;
        double distance = GetDistance(selfLongitude, selfLatitude, targetLongitude, targetLatitude);
        distance /= 100; //the unit at unity
        target = Instantiate(targetObject);
        target.transform.position = new Vector3(targetLongitude * (float)distance, 0
                                        , targetLatitude * (float)distance);
        //CaculateAngle(distance);
    }
    void CaculateAngle(double distance)
    {
        //double aDirectionX = 0;
        //double aDirectionY = 1;
        double bDirectionX = targetLatitude - selfLatitude;
        double bDirectionY = targetLongitude - selfLongitude;

        double dotProductValue = bDirectionY;
        double cosAngle = dotProductValue / distance;

    }
    /// <summary>
    /// return the distance(meter)
    /// </summary>
    /// <param name="lng1"></param>
    /// <param name="lat1"></param>
    /// <param name="lng2"></param>
    /// <param name="lat2"></param>
    /// <returns></returns>
    public static double GetDistance(double lng1, double lat1, double lng2, double lat2)
    {
        
        double radLat1 = Rad(lat1);
        double radLng1 = Rad(lng1);
        double radLat2 = Rad(lat2);
        double radLng2 = Rad(lng2);
        double a = radLat1 - radLat2;
        double b = radLng1 - radLng2;
        double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + 
                        Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
        return result;
        //source¡Ghttps://blog.csdn.net/xiaouncle/article/details/57084546
    }
    private static double Rad(double d)
    {
        return (double)d * Math.PI / 180d;
    }


}
