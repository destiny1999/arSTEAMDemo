using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Mapbox.Unity.Map;

//using Mapbox.CheapRulerCs;
//using Mapbox.Unity.Location;
//using Mapbox.Unity.Utilities;
//using Mapbox.Utils;
//using Mapbox.Examples;
//using Mapbox.Unity.MeshGeneration.Factories;
using System;

public class MapManager : MonoBehaviour
{
    bool showMap = true;
    [SerializeField] Camera MapCamera = null;
    [SerializeField] Camera PlayerCamera = null;
    //[SerializeField] AbstractMap _mapManager = null;
    [SerializeField] GameObject PlayerTarget = null;
    [SerializeField] GameObject AllMapObject = null;
    [SerializeField] GameObject MapObject = null;
    [SerializeField] float delayToStart = 5f;
    
    
    [SerializeField] TMP_Text TMP_playerCoordinat = null;
    [SerializeField] GameObject TargetMark = null;
    [SerializeField] List<GameObject> Buttons = new List<GameObject>();
    [SerializeField] GameObject MarkTargetInfoView = null;
    [SerializeField] GameObject DirectionsManager = null;
    [SerializeField] GameObject CatchScene = null;
    [SerializeField] GameObject MapInfoCanvas = null;
    [SerializeField] GameObject WelcomeView = null;


    
    //private AbstractLocationProvider _locationProvider = null;
    List<double[]> targets = new List<double[]>();
    bool setPosition = false;
    bool move = false;
    bool put = false;
    bool remove = false;
    bool isMapCamera = false;
    public static MapManager Instance;
    bool clickButton = false;
    GameObject showInfoTarget = null;

    //[SerializeField] AstronautDirections directions;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        //StartCoroutine(DisableWaitingPanel());
        //if (null == _locationProvider)
        //{
        //    _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
        //}
    }
    IEnumerator DisableWaitingPanel()
    {
        float time = 0;
        while(time < delayToStart)
        {
            time += Time.deltaTime * 1;
            yield return 0;
        }
        MapObject.SetActive(false);
        PlayerTarget.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        //Location currLoc = _locationProvider.CurrentLocation;

        //TMP_playerCoordinat.text = "目前位置 : 緯度 " + currLoc.LatitudeLongitude.x.ToString("0.000000") + " 經度 : " + currLoc.LatitudeLongitude.y.ToString("0.000000");

        

        UpdateDistance();
        
    }
    
    public IEnumerator SpawnTargetMark()
    {
        if (put)
        {

            var mousePosScreen = Input.mousePosition;

            mousePosScreen.z = MapCamera.transform.localPosition.y;
            var pos = MapCamera.ScreenToWorldPoint(mousePosScreen);

            //var latlongDelta = _mapManager.WorldToGeoPosition(pos);

            if (clickButton)
            {
                clickButton = false;
                yield break;
            }

            //CreateObjectAtClickPosition(latlongDelta.x, latlongDelta.y, pos, false);
        }
        
    }
    void UpdateDistance()
    {
        //Location currLoc = _locationProvider.CurrentLocation;
        
        //double[] self = { currLoc.LatitudeLongitude.y, currLoc.LatitudeLongitude.x };
        //CheapRuler cheapRuler = new CheapRuler(self[0], CheapRulerUnits.Meters);
        ////TMP_distance.text = "self lan :" + self[1];


        //foreach(GameObject mark in MapObject.GetComponent<SpawnOnMap>().GetSpawnedObjects())
        //{
        //    if (mark == null) continue;
        //    double[] coordinationData = mark.GetComponent<TargetMarkController>().GetCoordinationData();
        //    double distance = cheapRuler.Distance(coordinationData, self);
        //    mark.GetComponent<TargetMarkController>().SetDistanceData(distance);
        //}
    }
    void CreateObjectAtClickPosition(double lat, double lon, Vector3 pos, bool welcome)
    {

        //var mouseInput = Input.mousePosition;

        //mouseInput.z = MapCamera.transform.localPosition.y;
        //var mousePos = MapCamera.ScreenToWorldPoint(mouseInput);
        //var mouseLatLonData = _mapManager.WorldToGeoPosition(mousePos);
        //var lat2 = mouseLatLonData.x;
        //var lon2 = mouseLatLonData.y;

        

        //MapObject.GetComponent<SpawnOnMap>().AddCoordination(lat, lon, welcome);
        
        //setPosition = true;
        //double[] target = { lon, lat };
        //targets.Add(target);
    }
    
    public void MapVisiableSwitch()
    {
        showMap = !showMap;
        MapObject.SetActive(showMap);
        PlayerTarget.SetActive(showMap);

        MapCamera.gameObject.SetActive(showMap);
        CameraFollowPlayer.Instance.SetShowMap(showMap);
    }
    public void ClickButton(int index)
    {
        switch (index)
        {
            case 0:
                move = true;
                put = false;
                remove = false;
                break;
            case 1:
                move = false;
                put = true;
                remove = true;
                break;
            case 2:
                move = false;
                put = false;
                remove = true;
                break;
        }
    }
    public void ClickPut(GameObject BT_PutOK)
    {
        MapCamera.gameObject.GetComponent<MapCameraController>().ClickToFindSelf();
        MapCamera.gameObject.SetActive(true);
   
        PlayerCamera.gameObject.SetActive(false);

        SetShowCamera(MapCamera);
        BT_PutOK.SetActive(true);
        put = true;
        SetButtonsActive(false);
        SetMarkTargetCanClick(false);
        
    }
    void SetMarkTargetCanClick(bool status)
    {
        //MapObject.GetComponent<SpawnOnMap>().SetCanClickStatus(status);
    }
    void SetButtonsActive(bool status)
    {
        foreach(GameObject button in Buttons)
        {
            button.SetActive(status);
        }
    }
    public void ClickPutOK(GameObject BT_PutOK)
    {
        clickButton = true;
        PlayerCamera.gameObject.SetActive(true);
        MapCamera.gameObject.SetActive(false);

        SetShowCamera(PlayerCamera);
        put = false;
        BT_PutOK.SetActive(false);
        SetButtonsActive(true);
        SetMarkTargetCanClick(true);
        PlayerCameraController.Instance.SetMoveCubeAfterPutOK(MapCamera.transform.position);
        if(showInfoTarget != null)
        {
            ClickGuide();
        }
        
        //ClickFindSelf();
    }
    public void SetShowCamera(Camera target)
    {
        if (target.gameObject.name.Equals("MapCamera")) isMapCamera = true;
        else isMapCamera = false;
        //MapObject.GetComponent<AbstractMap>().SetCamera(target);
    }
    public void ShowInfoView(GameObject target)
    {
        showInfoTarget = target;
        MarkTargetInfoView.SetActive(true);
        SetMarkTargetCanClick(false);
    }
    public void SetShowInfoTarget(GameObject target)
    {
        showInfoTarget = target;
        //DirectionsManager.GetComponent<DirectionsFactory>().SetGuideWayPoints(PlayerTarget.transform, showInfoTarget.transform);
    }
    public void ClickGuide()
    {
        //DirectionsManager.GetComponent<DirectionsFactory>().SetGuideWayPoints(PlayerTarget.transform, showInfoTarget.transform);
        //DirectionsManager.GetComponent<DirectionsFactory>().enabled = true;
        //DirectionsManager.GetComponent<DirectionsFactory>().SetGuideStatus(true);
        MarkTargetInfoView.SetActive(false);
        SetMarkTargetCanClick(true);
    }
    public void ClickDelete(string targetName)
    {
        //DirectionsManager.GetComponent<DirectionsFactory>().SetGuideStatus(false);
        Destroy(GameObject.Find("direction waypoint  entity"));


        /*if (DirectionsManager.GetComponent<DirectionsFactory>().GetGuideTarget().Equals(showInfoTarget))
        {
            DirectionsManager.GetComponent<DirectionsFactory>().SetGuideStatus(false);
            Destroy(GameObject.Find("direction waypoint  entity"));
        }*/

        //string targetName = showInfoTarget.name;
        showInfoTarget = null;
        int index = Int32.Parse(targetName.Substring(4));
        
        Destroy(GameObject.Find(targetName));
        MarkTargetInfoView.SetActive(false);
        SetMarkTargetCanClick(true);
    }
    public void ClickCancel()
    {
        MarkTargetInfoView.SetActive(false);
        SetMarkTargetCanClick(true);
    }
    public void ClickFindSelf()
    {
        clickButton = true;
        if (isMapCamera)
        {
            MapCameraController.Instance.ClickToFindSelf();
        }
        else
        {
            PlayerCameraController.Instance.ClickToFindSelf();
        }
    }
    public void ShowCatchScene(bool show)
    {
        MapInfoCanvas.SetActive(!show);
        CatchScene.SetActive(show);
        if (show)
        {
            CatchSceneController.Instance.SpawnNewBall();
            
        }
        Destroy(GameObject.Find("direction waypoint  entity"));
        PlayerCamera.gameObject.SetActive(!show);
        MapCamera.gameObject.SetActive(false);
    }
    public void ClickSetting(GameObject SettingView)
    {
        SettingView.SetActive(true);
    }
    public void ClickSettingCancel(GameObject SettingView)
    {
        SettingView.SetActive(false);
    }
    public void ClickSettingConfirm(TMP_Text TMP_Selected)
    {
        var lat = 0.0;
        var lon = 0.0;
        switch (TMP_Selected.text)
        {
            case "不設定初始目標":
                SetButtonsActive(true);
                break;
            case "大南國小":
                //24.226683, 120.804481
                lat = 24.226683;
                lon = 120.804481;
                break;
        }
        TMP_Selected.transform.parent.parent.gameObject.SetActive(false);
        if(lat !=0 && lon != 0)
        {
            CreateObjectAtClickPosition(lat, lon, Vector3.zero, true);
        }
        GameObject.Find("BT_Setting").SetActive(false);
    }
    public void ShowWelcomeView()
    {
        SetButtonsActive(false);
        WelcomeView.SetActive(true);
    }
    public void ClickToHideWelcomeView(GameObject BT_Setting)
    {
        WelcomeView.SetActive(false);
        SetButtonsActive(true);
        Destroy(GameObject.Find("WelcomeObject"));
        //BT_Setting.SetActive(false);
    }
    public void ChangeZoom(float value)
    {

        //if (Math.Abs(value) < 0.1) return;
        //float zoom = MapObject.GetComponent<AbstractMap>().Zoom;

        //if (value > 0) value = 0.5f;
        //else value = -0.5f;

        //zoom += value;

        //if (zoom < 1) zoom = 1;
        //else if (zoom > 22) zoom = 22;

        //MapObject.GetComponent<AbstractMap>().SetZoom(zoom);
        
        //MapObject.GetComponent<AbstractMap>().UpdateMap();
    }
}
