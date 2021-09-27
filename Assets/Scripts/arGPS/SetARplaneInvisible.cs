using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class SetARplaneInvisible : MonoBehaviour
{
    [SerializeField] ARPlaneManager planeManager;
    private void Awake()
    {
        planeManager = this.GetComponent<ARPlaneManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetAllPlaneInvisible()
    {
        foreach(var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
