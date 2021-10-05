using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static VoiceManager Instance;
    [SerializeField] SampleSpeechToText sample;
    [SerializeField] InputField resultInputField;
    [SerializeField] GameObject ButtonVoice;
    HashSet<string> VoiceControllerName = new HashSet<string>();
    HashSet<string> allOrders = new HashSet<string>();
    Dictionary<string, int> orderAction = new Dictionary<string, int>();
    bool settingOrder = true;

    int orderIndex = -1;

    private void Awake()
    {
        Instance = this;
    }
    public void AddToNameSet(string _name)
    {
        VoiceControllerName.Add(_name);
    }
    public void ResetNameSet()
    {
        VoiceControllerName = new HashSet<string>();
    }
    public void SetOrder(string _order)
    {
        if (allOrders.Contains(_order)) return;
        allOrders.Add(_order);
        orderAction.Add(_order, orderIndex);

    }

    public void JudgeOrder()
    {
        
        string _order = resultInputField.text;

        if (settingOrder)
        {
            SetOrder(_order);
            return;
        }

        if (allOrders.Contains(_order))
        {
            int actoinIndex = orderAction[_order];
            ExecuteAction(actoinIndex);
        }
        sample.StopRecording();
        sample.StartRecording();
    }
    public void ExecuteAction(int actionIndex)
    {
        switch (actionIndex)
        {
            case 0:
                print("case 0, orderName = create");
                break;
        }
    }

    public void ClickOrderSet(int _orderIndex)
    {
        orderIndex = _orderIndex;
        ButtonVoice.SetActive(true);
    }
    public void ClickSetOrderOK()
    {
        ButtonVoice.SetActive(false);
        settingOrder = false;
    }
}
