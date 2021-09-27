using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChangeSceneManager
{
    public static string nextViewName = "";
    public static string GetNextNviewName()
    {
        return nextViewName;
    }
    public static void SetNextViewName(string _nextViewName)
    {
        nextViewName = _nextViewName;
    }
}
