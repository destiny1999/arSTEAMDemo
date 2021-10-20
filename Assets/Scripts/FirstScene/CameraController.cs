using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Camera = null;
    [SerializeField]Vector3 orignalPosition = Vector3.zero;
    [SerializeField]Quaternion orignalRotation = Quaternion.identity;
    [SerializeField] List<Vector3> cameraPosition = new List<Vector3>();
    [SerializeField] List<Vector3> cameraRotation = new List<Vector3>();
    [SerializeField] float speed = 5f;
    [SerializeField] float rotateSpeed = 360f;
    [SerializeField] GameObject BackToMain = null;
    [SerializeField] GameObject DetectObjects = null;
    [SerializeField] List<GameObject> itemsList = new List<GameObject>();
    [SerializeField] GameObject ProejctionTarget = null;
    [SerializeField] GameObject ProjectionObjects = null;
    public static CameraController Instance;
    int nowIndex = 0;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }
    public void ChangeToProjection(bool status)
    {
        BackToMain.SetActive(!status);
        itemsList[nowIndex].SetActive(!status);
        ProejctionTarget.SetActive(status);
        ProjectionObjects.SetActive(status);
    }
    
    // Update is called once per frame
    public void ChangePosition(int targetIndex)
    {
        if(targetIndex == 0)
        {
            itemsList[nowIndex].SetActive(false);
            BackToMain.SetActive(false);
            DetectObjects.SetActive(true);
        }
        else
        {
            DetectObjects.SetActive(false);
        }
        if(targetIndex != nowIndex)
        {
            nowIndex = targetIndex;
            StartCoroutine(GotoNextPosition());
        }
    }
    IEnumerator GotoNextPosition()
    {
        
        while(transform.position != cameraPosition[nowIndex] || transform.rotation.eulerAngles.y != cameraRotation[nowIndex].y)
        {
            int direction = cameraRotation[nowIndex].y > transform.rotation.eulerAngles.y ? 1 : -1;
            float step = speed * Time.deltaTime;
            
            transform.position = Vector3.MoveTowards(transform.position, cameraPosition[nowIndex], step);
            if(Mathf.Abs(cameraRotation[nowIndex].y - transform.rotation.eulerAngles.y) > 3)
            {
                transform.Rotate(new Vector3(0, direction, 0) * rotateSpeed * Time.deltaTime);
            }
            else
            {
                Quaternion _rotation = Quaternion.Euler(cameraRotation[nowIndex]);
                transform.rotation = _rotation;
            }
            
            yield return 1;
        }
        
        if (nowIndex != 0)
        {
            BackToMain.SetActive(true);
            itemsList[nowIndex].SetActive(true);
        }
    }
    public int GetNowIndex()
    {
        return nowIndex;
    }
}
