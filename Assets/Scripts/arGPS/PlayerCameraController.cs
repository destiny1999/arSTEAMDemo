//using Mapbox.Unity.Location;
//using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera PlayerCamera = null;
    [SerializeField] GameObject PlayerObject = null;
    //[SerializeField] AbstractMap _mapManager = null;
    [SerializeField] float cameraMoveSpeed = 1f;
    [SerializeField] GameObject moveCube = null;
    [SerializeField] bool ShouldFollowPlayer = true;
    [SerializeField] GameObject CameraMoveEmpty = null;
    //private AbstractLocationProvider _locationProvider = null;
    [SerializeField] int UpdateAfterChangeNTimes = 10;
    
    [SerializeField] bool follow = true;
    [SerializeField] Transform originalMoveCubeTransform = null;
    Vector3 _originalMoveCubeTransform;
    public static PlayerCameraController Instance;
    bool drag = false;

    Vector3 mouseInput;
    Vector3 mouseInput2;


    bool front = false;


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
        //_originalMoveCubeTransform = new Vector3(originalMoveCubeTransform.localPosition.x,
        //                                         originalMoveCubeTransform.localPosition.y,
        //                                         originalMoveCubeTransform.localPosition.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drag)
        {
            if (Input.touchCount >= 2)
            {
                mouseInput = Vector3.zero;
                drag = false;
                return;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                mouseInput = Vector3.zero;
                drag = false;
            }
            else
            {
                follow = false;
                //MoveCubeMove();
            }
        }


        if (Input.GetMouseButtonDown(0))
        {

            //StartCoroutine(JudgeClickOrDrag());
        }
    }
    void MoveCubeMove()
    {

        var mouseInput = Input.mousePosition;

        mouseInput.z = PlayerCamera.transform.localPosition.y;
        var mousePos = PlayerCamera.ScreenToWorldPoint(mouseInput);

        int frontOrBack = 0;
        if(mouseInput.y - mouseInput2.y > 0)
        {
            front = true;
            frontOrBack = 1;
        }
        else if (mouseInput.y - mouseInput2.y < 0)
        {
            front = false;
            frontOrBack = -1;
        }
        else
        {
            frontOrBack = front ? 1 : -1;
        }
        

        /*
        if (mouseInput2.Equals(Vector3.zero))
        {
            if(mouseInput.y >= Screen.height / 2)
            {
                frontOrBack = 1;
            }
            else
            {
                frontOrBack = -1;
            }
        }
        else
        {
            if(Mathf.Abs(mouseInput.y - mouseInput2.y) > 5)
            {
                frontOrBack = mouseInput.y > mouseInput2.y ? 1 : -1;
            }
            
        }*/
        /*if (frontOrBack > 0)
        {
            moveCube.transform.Translate(moveCube.transform.forward);
        }
        else
        {
            moveCube.transform.Translate(-moveCube.transform.forward);
        }
        if(rightOrLeft > 0)
        {
            moveCube.transform.Translate(moveCube.transform.right);
        }
        else
        {
            moveCube.transform.Translate(-moveCube.transform.right);
        }*/

        float targetZ = frontOrBack > 0 ? moveCube.transform.position.z + 1 : moveCube.transform.position.z - 1;
        
        Vector3 targetPosition = new Vector3(mousePos.x, moveCube.transform.position.y, moveCube.transform.position.z);
        

        moveCube.transform.position = Vector3.MoveTowards(moveCube.transform.position, targetPosition, cameraMoveSpeed);
        if (frontOrBack > 0)
        {
            moveCube.transform.Translate(moveCube.transform.forward);
        }
        else
        {
            moveCube.transform.Translate(-moveCube.transform.forward);
        }

        mouseInput2 = mouseInput;



        /*

        if (mouseInput == Vector3.zero)
        {
            mouseInput = Input.mousePosition;

            mouseInput.z = PlayerCamera.transform.localPosition.y;
        }
        else
        {
            mouseInput2 = Input.mousePosition;
            mouseInput.z = PlayerCamera.transform.localPosition.y;

            if (mouseInput != mouseInput2)
            {
                float tempX = mouseInput2.x - mouseInput.x > 0 ? mouseInput2.x - mouseInput.x : mouseInput2.x - mouseInput.x == 0 ? 0 : mouseInput2.x - mouseInput.x;
                float tempY = mouseInput2.y - mouseInput.y > 0 ? mouseInput2.y - mouseInput.y : mouseInput2.y - mouseInput.y == 0 ? 0 : mouseInput2.y - mouseInput.y;

                if (tempX != 0 || tempY != 0)
                {
                    moveX = tempX;
                    moveZ = tempY;
                }
            }
            moveCube.transform.Translate(moveX * Time.deltaTime * cameraMoveSpeed, 0, moveZ * Time.deltaTime * cameraMoveSpeed);
            mouseInput = mouseInput2;
        }*/
    }
    IEnumerator JudgeClickOrDrag()
    {

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
            mouseInput2 = Input.mousePosition;
            moveCube.transform.SetParent(CameraMoveEmpty.transform);
        }

    }
    public void ClickToFindSelf()
    {
        follow = true;
        moveCube.transform.SetParent(PlayerObject.transform);
        moveCube.transform.localRotation = Quaternion.identity;
        moveCube.transform.localPosition = _originalMoveCubeTransform;
        //_mapManager.UpdateMap();
    }

    public void SetFollow(bool status)
    {
        follow = status;
    }
    public void SetMoveCubeAfterPutOK(Vector3 targetPosition)
    {
        ClickToFindSelf();
        /*if (follow)
        {
            
            return;
        }*/
        //moveCube.transform.localPosition = new Vector3(targetPosition.x, 0, targetPosition.z-18);
    }
}
