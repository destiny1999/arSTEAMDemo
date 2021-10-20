using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClickDetector : MonoBehaviour
{
    // Start is called before the first frame update
    Ray ray;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.name.Equals("FrontDetect"))
                {
                    CameraController.Instance.ChangePosition(1);
                }
                else if (hit.collider.name.Equals("LeftDetect"))
                {
                    CameraController.Instance.ChangePosition(2);
                }
                else if (hit.collider.name.Equals("RightDetect"))
                {
                    CameraController.Instance.ChangePosition(3);
                }
                //print(hit.collider.name);
            }
        }
    }
    
}
