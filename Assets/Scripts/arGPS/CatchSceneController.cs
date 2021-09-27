using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchSceneController : MonoBehaviour
{
    [SerializeField] Transform catchBallPosition;
    [SerializeField] GameObject catchBall = null;
    [SerializeField] Camera SceneCamera = null;
    public static CatchSceneController Instance;
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
    public void SpawnNewBall()
    {
        print("new ball");
        GameObject newBall = Instantiate(catchBall);
        newBall.transform.SetParent(catchBallPosition);
        newBall.GetComponent<CatchBallSetting>().SetReferenceCamera(SceneCamera);
        newBall.transform.localPosition = Vector3.zero;
    }
}
