using ARLocation;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class TargetDetectController : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mainCamera;
    private bool hasArLocationManager;
    private ARLocationManager arLocationManager;
    [SerializeField][Tooltip("This audio will play when player into the range (notice distance)")]
    AudioSource IntoRangeAudio = null;
    [SerializeField] float NoticeDistance = 100;
    [SerializeField][Tooltip("When player into the range it will show the object")] float ShowObjectDistance = 5;
    [SerializeField] GameObject targetObject = null;
    void Start()
    {
        mainCamera = ARLocationManager.Instance.MainCamera;

        arLocationManager = ARLocationManager.Instance;
        hasArLocationManager = arLocationManager != null;
    }

    // Update is called once per frame
    void Update()
    {
        
        var floorLevel = hasArLocationManager ? arLocationManager.CurrentGroundY : -ARLocation.ARLocation.Config.InitialGroundHeightGuess;
        var startPos = MathUtils.SetY(mainCamera.transform.position, floorLevel);
        var endPos = MathUtils.SetY(transform.position, floorLevel);

        var distance = Vector3.Distance(startPos, endPos);

        if(distance <= NoticeDistance)
        {
            if (!IntoRangeAudio.isPlaying)
            {
                IntoRangeAudio.Play();
            }
            
        }
        else
        {
            IntoRangeAudio.Pause();
        }
        if(distance <= ShowObjectDistance)
        {
            targetObject.SetActive(true);
        }
        else
        {
            targetObject.SetActive(false);
        }
    }
}
