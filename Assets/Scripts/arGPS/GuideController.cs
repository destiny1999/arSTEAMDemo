using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject target = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target.transform);
        
    }
}
