using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EventLog : MonoBehaviour
{
    private List<string> Log = new List<string>();
    private string text = "";

    public int maxLines = 10;

    void OnGUI()
    {
        GUI.Label(new Rect(0, Screen.height - (Screen.height / 3), Screen.width, Screen.height / 3), text, GUI.skin.textArea);
    }

    public void AddEvent(string eventString)
    {
        Log.Add(eventString);

        if (Log.Count >= maxLines)
            Log.RemoveAt(0);

        text = "";

        foreach (string logEvent in Log)
        {
            text += logEvent;
            text += Environment.NewLine;
        }
    }
}