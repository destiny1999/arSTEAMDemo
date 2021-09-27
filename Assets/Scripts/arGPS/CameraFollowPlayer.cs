//using Mapbox.Unity.Location;
//using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera MapCamera = null;
    [SerializeField] GameObject PlayerObject = null;
    //[SerializeField] AbstractMap _mapManager = null;
    [SerializeField] float cameraMoveSpeed = 1f;
    [SerializeField] GameObject testMoveObject = null;
    [SerializeField] bool ShouldFollowPlayer = true;
    //private AbstractLocationProvider _locationProvider = null;
    [SerializeField] int UpdateAfterChangeNTimes = 10;
    //int updateTimes = 0;
    //bool showMap = true;
    bool follow = true;
    //float delayTime = 0;
    public static CameraFollowPlayer Instance;
    bool drag = false;
    bool isMapManager = false;
    // to detect the pos about poniter to move camera
    //Vector3 mousePos1 = new Vector3();
    //Vector3 mousePos2 = new Vector3();

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //if (null == _locationprovider)
        //{
        //    _locationprovider = locationproviderfactory.instance.defaultlocationprovider as abstractlocationprovider;
        //}
    }

    // Update is called once per frame
    void Update()
    {

        //this.transform.Translate(new Vector3(0, 1, 0));
        

        if (drag)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                print("mouse up");

                drag = false;
            }
            else
            {
                

                follow = false;

                var mouseInput = Input.mousePosition;

                mouseInput.z = MapCamera.transform.localPosition.y;
                var mousePos = MapCamera.ScreenToWorldPoint(mouseInput);

                testMoveObject.transform.position = new Vector3(mousePos.x, MapCamera.transform.position.y, mousePos.z);
                MapCamera.transform.position = Vector3.MoveTowards(MapCamera.transform.position, testMoveObject.transform.position, cameraMoveSpeed);
                //MapCamera.transform.position = new Vector3(testMoveObject.transform.position.x, MapCamera.transform.position.y, testMoveObject.transform.position.z);
            }
        }
        if (ShouldFollowPlayer)
        {
            //print("should follow");
            if (follow)
            {
                float targetX = PlayerObject.transform.position.x;
                float targetZ = PlayerObject.transform.position.z;

                if (targetX != MapCamera.transform.position.x || targetZ != MapCamera.transform.position.z)
                {
                    MapCamera.transform.position = new Vector3(targetX, MapCamera.transform.position.y, targetZ);
                    //updateMap = true;
                }

            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            //print("judge");
            StartCoroutine(JudgeClickOrDrag());
        }
    }
    IEnumerator JudgeClickOrDrag()
    {
        
        float delayTime = 0.3f;
        float time = 0f;
        bool dragging = true;
        while(time < delayTime)
        {
            time += Time.deltaTime * 1;
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                dragging = false;
                break;
            }
            yield return 1;
        }
        if (dragging)
        {
            drag = true;
        }
        
    }
    public void ClickToFindSelf()
    {
        follow = true;
    }
    public void SetShowMap(bool status)
    {
        //showMap = status;
        /*if (showMap)
        {
            UpdateMap();
        }*/
    }
    public void SetFollow(bool status)
    {
        follow = status;
    }
    /*void UpdateMap()
    {

        Location currLoc = _locationProvider.CurrentLocation;

        Vector2d v2d = new Vector2d();
        v2d.x = currLoc.LatitudeLongitude.x;
        v2d.y = currLoc.LatitudeLongitude.y;

        //_mapManager.UpdateMap(v2d);
    }*/
}
