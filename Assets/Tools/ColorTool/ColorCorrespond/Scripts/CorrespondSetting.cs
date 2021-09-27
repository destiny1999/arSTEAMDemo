using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrespondSetting : MonoBehaviour
{
    bool canColored = true;
    [SerializeField] GameObject CorrespondPartner = null;

    float downTime = 0f;

    private void Start()
    {
        

    }
    private void OnMouseDown()
    {
        downTime = Time.time;
        
    }
    private void OnMouseUp()
    {
        var temp = Time.time - downTime;
        if(temp <= 1f)
        {
            Color changeColor = ColorCorrespond.Instance.GetColor();
            if (canColored)
            {
                gameObject.GetComponent<Renderer>().material.color = changeColor;
                //gameObject.GetComponent<Material>().color = changeColor;
                CorrespondPartner.GetComponent<Renderer>().material.color = changeColor;
                
            }
        }
        
    }
    public void SetCanColored(bool status)
    {
        canColored = status;
    }
}
