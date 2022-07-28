using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestAC : MonoBehaviour
{
    public Transform tagPoint;
    
    void Start()
    {
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Debug.Log("左键点击:" + Mouse.current.position.ReadValue());
            UIManager.Instance.ShowTip("提示测试", tagPoint.position);
        }
        else if (Mouse.current.rightButton.wasPressedThisFrame) {
            Debug.Log("右键点击:" + Mouse.current.position.ReadValue());
            UIManager.Instance.HidePanel("Tip", true);
        }
    }
}
