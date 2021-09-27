using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTouchController : MonoBehaviour
{
    [SerializeField] bool zoom = true;
    float changeTime = 0.1f;
    float _changeTime = 0;
    Touch oldTouch1;
    Touch oldTouch2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (zoom)
        {
            Zooming();
        }
    }
    void Zooming()
    {
        if(Input.touchCount < 2)
        {
            return;
        }
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        if(touch2.phase == TouchPhase.Began)
        {
            oldTouch1 = touch1;
            oldTouch2 = touch2;
            return;
        }

        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(touch1.position, touch2.position);

        float offset = newDistance - oldDistance;

        float scaleFactor = offset / 100f;
        _changeTime += Time.deltaTime * 1;
        if(_changeTime >= changeTime)
        {
            MapManager.Instance.ChangeZoom(scaleFactor);
            _changeTime = 0;
        }
        

        oldTouch1 = touch1;
        oldTouch2 = touch2;
    }
}
