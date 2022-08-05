using System;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneSpecific
{
    public class P0Manager : MonoBehaviour
    {
        public Camera camera;
        public new Transform light;
        
        private void Start()
        {
            UIManager.Instance.ShowPanel<StartPanel>("StartPanel");
        }

        private void Update()
        {
            // 在鼠标位置发光
            Vector3 pos = camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pos.z = 0;
            light.position = pos;
        }
    }
}
