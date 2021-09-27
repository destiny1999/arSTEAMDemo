using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    string nextViewName = "";
    static float progressValue = 0f;
    void Start()
    {
        nextViewName = ChangeSceneManager.GetNextNviewName();
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadScene()
    {
        

        AsyncOperation async = null;
        async = SceneManager.LoadSceneAsync(nextViewName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                progressValue = async.progress;
            else
                progressValue = 1.0f;

            //slider.value = progressValue;

            //progress.text = (int)(slider.value * 100) + " %";

            if (progressValue >= 0.9)
            {
                //progress.text = "100 %";
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
