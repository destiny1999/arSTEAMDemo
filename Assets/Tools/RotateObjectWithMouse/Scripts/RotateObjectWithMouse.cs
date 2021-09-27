using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateObjectWithMouse : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField][Tooltip("The rotate degree about mouse move")] float rotateWeight = 12f;
    [SerializeField][Tooltip("The value if mouse move over that will rotate")] float valve = 3f;
    Vector2 oldMousePosition = Vector2.zero;
    Vector2 nowMousePosition = Vector2.zero;
    bool press = false;
    bool canRotaet = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (canRotaet)
        {
            if (Input.GetMouseButtonDown(0))
            {
                oldMousePosition = Input.mousePosition;
                press = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                press = false;
            }
            if (press)
            {
                RotateObject();
            }
        }
    }
    void RotateObject()
    {
        nowMousePosition = Input.mousePosition;
        if(nowMousePosition == oldMousePosition)
        {
            return;
        }

        float yMove = Mathf.Abs(nowMousePosition.x - oldMousePosition.x) > valve ? nowMousePosition.x - oldMousePosition.x : 0;
        
        float xMove = Mathf.Abs(nowMousePosition.y - oldMousePosition.y) > valve ? nowMousePosition.y - oldMousePosition.y : 0;

        transform.Rotate(-yMove * Vector3.up * rotateWeight * Time.deltaTime, Space.World);
        transform.Rotate(xMove * Vector3.right * rotateWeight * Time.deltaTime, Space.World);

        oldMousePosition = nowMousePosition;
    }
    public void SetCanRotate(bool status)
    {
        canRotaet = status;
    }
}
