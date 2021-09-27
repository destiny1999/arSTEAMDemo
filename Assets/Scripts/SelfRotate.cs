using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] bool UI = true;
    [SerializeField] RotateDirection rotateDirection;
    string[] directions = { "x", "y", "z" };
    string selectedDirection = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateSelf();
    }
    void RotateSelf()
    {
        selectedDirection = directions[(int)rotateDirection];
        float x= 0, y=0, z = 0;
        switch (selectedDirection)
        {
            case "x":
                x = rotateSpeed * Time.deltaTime;
                break;
            case "y":
                y = rotateSpeed * Time.deltaTime;
                break;
            case "z":
                z = rotateSpeed * Time.deltaTime;
                break;
        }
        if (UI)
        {
            RotateUI(x, y, z);
        }
        else
        {
            RotateObject(x, y, z);
        }
    }
    void RotateUI(float x, float y, float z)
    {
        this.GetComponent<RectTransform>().Rotate(x, y, z);
    }
    void RotateObject(float x, float y, float z)
    {
        this.GetComponent<Transform>().Rotate(x, y, z);
    }
    enum RotateDirection
    {
        x,
        y,
        z
    }

}
