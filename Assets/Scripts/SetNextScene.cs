using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetAndChangeScene(string _nextSceneName)
    {
        ChangeSceneManager.nextViewName = _nextSceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadScene");
    }
}
