using System;
using System.Collections;
using System.Collections.Generic;
using Save;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestAC : MonoBehaviour
{
    public Transform tagPoint;
    
    void Start()
    {
        Debug.Log(int.Parse("5"));
        Debug.Log(bool.Parse("True"));
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
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["test1"] = "测试1";
            dic["test2"] = "测试2";
            SaveManager.Persistence.Write(dic, SaveManager.GetFullFilePath("test"));
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
