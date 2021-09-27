using GoMap;
using GoShared;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoordinateManager : MonoBehaviour
{
    public static CoordinateManager Instance;
    [SerializeField] GameObject MarkerPrefabs = null;
    GameObject target = null;
    public bool press = false;
    Vector2 clickPos = Vector2.zero;
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
        PressToSpawnTarget();
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            press = false;
        }
    }
    void PressToSpawnTarget()
    {
        //Position
        if (Input.GetMouseButtonDown(0))
        {
            clickPos = Input.mousePosition;
            StartCoroutine(JudgePress());
            
        }
    }
    IEnumerator JudgePress()
    {
        float time = 0f;
        float delayTime = 1f;
        while (time < delayTime)
        {
            if (Mathf.Abs(Input.mousePosition.x - clickPos.x) > 3 || Mathf.Abs(Input.mousePosition.y - clickPos.y) > 3
                || Input.GetMouseButtonUp(0))
            {
                press = false;
                yield break;
            }
            time += Time.deltaTime * 1;
            yield return 1;
        }
        press = true;
        if (target != null)
        {
            Destroy(target);
        }
        target = Instantiate(MarkerPrefabs);



        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GOMap.GetActiveMasks()))
        {

            //From the raycast data it's easy to get the vector3 of the hit point 
            Vector3 worldVector = hit.point;
            //And it's just as easy to get the gps coordinate of the hit point.
            Coordinates gpsCoordinates = Coordinates.convertVectorToCoordinates(hit.point);
            target.transform.position = worldVector;
        }
    }
}
