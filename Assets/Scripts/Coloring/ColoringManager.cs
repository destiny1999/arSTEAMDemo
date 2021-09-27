using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoringManager : MonoBehaviour
{
    
    //[SerializeField] Camera webcame = null;
    [SerializeField] GameObject ARObjects = null;
    [SerializeField] Renderer targetModelMesh = null;
    
    public static ColoringManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Renderer GetPCModelToColoring()
    {
        return targetModelMesh;
    }
}
