using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecognizeTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    [SerializeField] SampleSpeechToText sample;
    [SerializeField] InputField message = null;
    HashSet<string> targetName = new HashSet<string>();

    public void OnPointerDown(PointerEventData eventData)
    {
        sample.StartRecording();

        //throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        //sample.StartRecording();
        //message.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
