using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using KDTree;
using System;
using TMPro;
//using Mapbox.Unity.MeshGeneration.Factories;

public class TargetMarkController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool canClick = false;
    [SerializeField] Material IntoCatchColor = null;
    [SerializeField] Material NormalColor = null;
    [SerializeField] float canCatchDistance = 3f;
    [SerializeField] TMP_Text TMP_latlonData = null;
    [SerializeField] TMP_Text TMP_distance = null;
    bool isWelcome = false;
    bool canCatch = false;
    double lat = 0;
    double lon = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //JudgeDistance();
    }
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.name);
        if (collision.transform.tag.Equals("Player"))
        {
            MapManager.Instance.ShowCatchScene(true);
            Destroy(this.gameObject);
        }

    }
    public void OnMouseDown()
    {
        
        if (!canClick) return;
        if (canCatch)
        {
            if (isWelcome)
            {
                MapManager.Instance.ShowWelcomeView();
                return;
            }
            else
            {
                MapManager.Instance.ShowCatchScene(true);
                //if (GameObject.Find("Directions").GetComponent<DirectionsFactory>().GetGuideTarget().Equals(this.gameObject))
                //{
                //    GameObject.Find("Directions").GetComponent<DirectionsFactory>().SetGuideStatus(false);
                //    Destroy(GameObject.Find("direction waypoint  entity"));
                //}
                Destroy(this.gameObject);
                return;
            }
        }
        //MapManager.Instance.ShowInfoView(this.gameObject);
    }
    public void SetCanClickStatus(bool status)
    {
        canClick = status;
    }
    public double[] GetCoordinationData()
    {
        double[] data = { lon, lat };
        return data;
    }
    public void SetLatLonData(double _lat, double _lon)
    {
        lat = _lat;
        lon = _lon;
        TMP_latlonData.text = "¸g«× : " + lon.ToString("0.000000") + "\r\n½n«× : " + lat.ToString("0.000000");
    }/*
    public void SetDistanceData(double distance)
    {
        if(distance > 1000)
        {
            distance /= 1000;
            TMP_distance.text = "¶ZÂ÷ : " + distance.ToString("0.0") + " km";
        }
        else
        {
            TMP_distance.text = "¶ZÂ÷ : " + Mathf.Floor((float)distance) + " m";
            if(distance <= canCatchDistance)
            {
                transform.Find("Cube").GetComponent<MeshRenderer>().material = IntoCatchColor;
                canCatch = true;
            }
            else
            {
                if(transform.Find("Cube").GetComponent<MeshRenderer>().material != NormalColor)
                {
                    transform.Find("Cube").GetComponent<MeshRenderer>().material = NormalColor;
                    canCatch = false;
                }
            }
        }
    }*/
    public void SetInfoStatus(bool status)
    {
        TMP_latlonData.gameObject.SetActive(status);
        TMP_distance.gameObject.SetActive(status);
    }
    public void SetIsWelcome(bool status)
    {
        isWelcome = status;
        if (isWelcome)
        {
            canClick = true;
        }
    }
}
