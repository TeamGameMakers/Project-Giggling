using System;
using System.Collections;
using System.Collections.Generic;
using Data.Story;
using Save;
using Story;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TestAC : MonoBehaviour
{
    public Transform tagPoint;

    public PlotDataSO plot;
    
    void Start()
    {
        StoryManager.Instance.StartStory(plot);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            
        }
        else if (Mouse.current.rightButton.wasPressedThisFrame) {
            
        }

        if (Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            Debug.Log("按下 1");
            foreach (var VARIABLE in UIManager.Instance.panelContainer)
            {
                Debug.Log($"{VARIABLE.Key}--{VARIABLE.Value}");
            }
        }
        else if (Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            Debug.Log("按下 2");
            Dictionary<string, string> dic = 
                SaveManager.Persistence.Read<Dictionary<string, string>>(SaveManager.GetFullFilePath("test"));
            foreach (var VARIABLE in dic)
            {
                Debug.Log($"{VARIABLE.Key}--{VARIABLE.Value}");
            }
        }
        else if (Keyboard.current.numpad3Key.wasPressedThisFrame)
        {
            Debug.Log("按下 3");
            
        }
    }
}
