using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool testMode = false;
    [SerializeField] GameObject TestModeCanvas = null;
    void Start()
    {
        if (testMode)
        {
            TestModeCanvas.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickChangeViewButton(string _nextViewName)
    {
        ChangeSceneManager.SetNextViewName(_nextViewName);
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadScene");
    }
}
