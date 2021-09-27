using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRotateController : MonoBehaviour
{

    [SerializeField] float _rotateValve = 30;
    [SerializeField] float rotateSpeed = 30f;
    [SerializeField] float currentRotateValue = 0;
    [SerializeField] TMP_Text selfRotation = null;
    [SerializeField] TMP_Text rotationValue = null;
    bool directionLeft = true;
    void Start()
    {
        currentRotateValue = Input.compass.trueHeading;

    }

    // Update is called once per frame
    void Update()
    {
        selfRotation.text = this.transform.rotation.eulerAngles.y+"";
        rotationValue.text = currentRotateValue+"";
        StartCoroutine(JudgeDirection());


    }
    IEnumerator JudgeDirection()
    {
        float oldValue = Input.compass.trueHeading;
        float value = Input.compass.trueHeading;
        float time = 0;
        bool change = true;
        while(time < 1f)
        {
            time += Time.deltaTime * 1;
            if (Mathf.Abs(currentRotateValue - value) < _rotateValve)
            {
                change = false;
                yield break;
            }
            value = Input.compass.trueHeading;
            yield return 1;
        }
        if (change)
        {

            currentRotateValue = value;
            //this.transform.rotation = Quaternion.Euler(0, currentRotateValue, 0);
            
            StartCoroutine(ChangeDirection());
            //this.transform.Rotate(new Vector3(0, currentRotateValue, 0) * Time.deltaTime * 30);
            //this.transform.eulerAngles = new Vector3(0, currentRotateValue, 0);
        }


    }
    IEnumerator ChangeDirection()
    {
        float nowY = transform.rotation.eulerAngles.y;
        nowY = nowY >= 0 ? nowY : (360 + nowY);
        float angle = currentRotateValue - nowY;

        while(Mathf.Abs(transform.rotation.eulerAngles.y - currentRotateValue) > 1)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, currentRotateValue, 0), rotateSpeed * Time.deltaTime);
            yield return 1;
        }
    }
}
