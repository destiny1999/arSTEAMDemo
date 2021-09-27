using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCorrespond : MonoBehaviour
{
    // Start is called before the first frame update
    Color nowColor = new Color();
    [SerializeField] List<Correspond> correspondList = new List<Correspond>();

    public static ColorCorrespond Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Color GetColor()
    {
        nowColor = ColorPicker.Instance.GetSelectedColor();
        return nowColor;
    }

}
[Serializable]
public class Correspond
{
    public GameObject selected;
    public GameObject correspond;
}
