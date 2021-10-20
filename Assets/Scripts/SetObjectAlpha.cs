using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectAlpha : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float alpha = 0;
    void Start()
    {
        ChangeAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeAlpha()
    {
        Color newColor = this.GetComponent<Renderer>().material.color;
        newColor.a = alpha;
        this.GetComponent<Renderer>().material.SetColor("_Color",newColor);
    }
}
