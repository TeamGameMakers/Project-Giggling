using UnityEngine;
using UnityEngine.InputSystem;

namespace Interact
{
    public class Peek : MonoBehaviour
    {
        public new Camera camera;
        public Transform maskLight;

        private void Update()
        {
            Vector3 pos = camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pos.z = 0;
            maskLight.position = pos;
        }
    }
}
