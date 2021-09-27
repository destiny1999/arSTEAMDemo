//using Mapbox.Unity.Location;
//using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera MapCamera = null;
    [SerializeField] GameObject PlayerObject = null;
    //[SerializeField] AbstractMap _mapManager = null;
    [SerializeField] float cameraMoveSpeed = 1f;
    [SerializeField] GameObject testMoveObject = null;

    //private AbstractLocationProvider _locationProvider = null;



    public static MapCameraController Instance;
    bool drag = false;
    float putTargetTime = 0.7f;
    bool putTarget = false;
    bool move = false;
    Vector3 mouseInput2 = Vector3.zero;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //if (null == _locationProvider)
        //{
        //    _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
        //}
    }

    // Update is called once per frame
    void Update()
    {

        if (drag)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {

                drag = false;
            }
            else
            {

                var mouseInput = Input.mousePosition;
                if (mouseInput.Equals(mouseInput2) && !move)
                {
                    StartCoroutine(WaitTimeToCreateTarget());
                }
                else
                {
                    move = true;
                }
                if (move)
                {
                    mouseInput.z = MapCamera.transform.localPosition.y;
                    var mousePos = MapCamera.ScreenToWorldPoint(mouseInput);

                    testMoveObject.transform.position = new Vector3(mousePos.x, MapCamera.transform.position.y, mousePos.z);
                    MapCamera.transform.position = Vector3.MoveTowards(MapCamera.transform.position, testMoveObject.transform.position, cameraMoveSpeed);
                }
                
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(JudgeClickOrDrag());
        }
    }
    IEnumerator WaitTimeToCreateTarget()
    {
        float time = 0;
        while(time < putTargetTime)
        {
            time += Time.deltaTime * 1;
            if (move) yield break;
            yield return 1;
        }
        if (!move && !putTarget)
        {

            StartCoroutine(MapManager.Instance.SpawnTargetMark());
            putTarget = true;
        }
    }
    IEnumerator JudgeClickOrDrag()
    {
        putTarget = true;

        mouseInput2 = Input.mousePosition;

        float delayTime = 0.3f;
        float time = 0f;
        bool dragging = true;
        while (time < delayTime)
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
            move = false;
            putTarget = false;
        }

    }
    public void ClickToFindSelf()
    {
        this.transform.position = new Vector3(PlayerObject.transform.position.x, this.transform.position.y, PlayerObject.transform.position.z);
    }
}
