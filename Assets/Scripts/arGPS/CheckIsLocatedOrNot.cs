using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsLocatedOrNot : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position != Vector3.zero)
        {
            this.GetComponent<TargetDetectController>().enabled = true;
            this.enabled = false;
        }
    }
}
