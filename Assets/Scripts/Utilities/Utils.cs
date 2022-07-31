using UnityEngine;
using UnityEngine.InputSystem;

namespace Utilities
{
    public static class Utils
    {
        /// <summary>
        /// 在场景创建文字
        /// </summary>
        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 position, TextAnchor textAnchor, TextAlignment textAlignment,
            int fontSize, float characterSize, Color color, int sortingOrder = 5000) 
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.position = position;
            transform.SetParent(parent, true);
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.characterSize = characterSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }

        public static Vector3 GetMouseWorldPositionWithZ(Vector2 screenPosition, Camera worldCamera) 
            => worldCamera.ScreenToWorldPoint(screenPosition);

        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera = null)
        {
            Vector2 mousePosition;

            #if ENABLE_INPUT_SYSTEM
            mousePosition = Mouse.current.position.ReadValue();
            #else
            mousePosition = Input.mousePosition;
            #endif

            return worldCamera ? GetMouseWorldPositionWithZ(mousePosition, worldCamera)
                    : GetMouseWorldPositionWithZ(mousePosition, Camera.main);
        }


        public static Vector3 GetMouseWorldPosition()
        {
            var vec = GetMouseWorldPositionWithZ();
            vec.z = 0f;
            return vec;
        }
    }
}