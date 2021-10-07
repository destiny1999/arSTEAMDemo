using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceOrderController : MonoBehaviour
{
    // Start is called before the first frame update
    public static VoiceOrderController Instance;
    Dictionary<string, int> orderAction = new Dictionary<string, int>();
    [SerializeField]
    List<string> OrderString = new List<string>();
    [SerializeField]
    GameObject MainCamera;
    bool changeFinished = true;
    [SerializeField]
    bool testModeUseKeyBoardControl = false;
    [SerializeField]
    GameObject ShowObject;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        InitializeOrder();
    }
    void InitializeOrder()
    {
        for(int i = 0; i<OrderString.Count; i++)
        {
            orderAction.Add(OrderString[i], i);
        }
    }
    private void Update()
    {
        if (testModeUseKeyBoardControl)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeRoom(1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeRoom(-1);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ChangeSize(1);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                ChangeSize(-1);
            }
        }
        
    }
    public void JudgeOrder(Text order)
    {
        string judgeString = order.text;

        string[] judgeStrings = judgeString.Split(' ');

        for(int i = 0; i<judgeStrings.Length; i++)
        {
            if (orderAction.ContainsKey(judgeStrings[i]))
            {
                ExecuteAction(orderAction[judgeStrings[i]]);
                break;
            }
        }
        
    }
    void ExecuteAction(int actionCode)
    {
        switch (actionCode)
        {
            case 0://zoom in
                ChangeRoom(1);
                break;
            case 1://zoom out
                ChangeRoom(-1);
                break;
            case 2://large size
                ChangeSize(1);
                break;
            case 3://small size
                ChangeSize(-1);
                break;
        }
    }
    void ChangeSize(int value)
    {
        StartCoroutine(TargetSizeChange(value));
    }
    IEnumerator TargetSizeChange(int changeSize)
    {
        if (!changeFinished)
        {
            yield break;
        }
        changeFinished = false;
        float targetSize = ShowObject.transform.localScale.x + (float)changeSize * 0.1f;
        while (Mathf.Abs(ShowObject.transform.localScale.x - targetSize) > 0.01f)
        {
            float newSize = ShowObject.transform.localScale.x + Time.deltaTime * changeSize;    
            ShowObject.transform.localScale = new Vector3(newSize, newSize, newSize);

            yield return 1;
        }
        ShowObject.transform.localScale = new Vector3(targetSize, targetSize, targetSize);
        changeFinished = true;

    }
    void ChangeRoom(int value)
    {
        StartCoroutine(CameraMoveZ(value));

    }
    IEnumerator CameraMoveZ(float direction)
    {
        if (!changeFinished)
        {
            yield break;
        }
        Vector3 cameraVector = MainCamera.transform.position;
        float targetZ = MainCamera.transform.position.z + direction;

        while ( Mathf.Abs(MainCamera.transform.position.z - targetZ) > 0.1 )
        {
            changeFinished = false;

            MainCamera.transform.Translate(Vector3.forward * direction * Time.deltaTime);
            yield return 1;
        }
        MainCamera.transform.position = new Vector3(cameraVector.x, cameraVector.y, targetZ);
        changeFinished = true;
    }
    
}
