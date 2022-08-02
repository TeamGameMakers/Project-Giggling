using UnityEngine;
using UnityEngine.InputSystem;

namespace Utilities
{
    public static class Utils
    {
        /// <summary>
        /// 绘制圆弧Gizmos
        /// </summary>
        public static void DrawWireArc(Transform origin, float radius, float angle, Color color,
            int segments = 50, float deviation = 0)
        {
            Gizmos.color = color;
            var offset = Vector3.up * deviation;
            var dir = Quaternion.AngleAxis(angle, origin.up) * origin.forward * radius + offset;

            var minDeg = angle * 2 / segments;
            var currentPos = origin.position + dir;

            if (angle - 180 < 0.001f)
                Gizmos.DrawLine(origin.position + offset, currentPos);

            for (int i = 0; i <= segments; i++)
            {
                var oldPos = currentPos;
                currentPos = origin.position + Quaternion.AngleAxis(-i * minDeg, origin.up) * dir;
                Gizmos.DrawLine(oldPos, currentPos);
            }

            if (angle - 180 < 0.001f)
                Gizmos.DrawLine(origin.position + offset, currentPos);

        }

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
        
        // 单位向量点乘得到角度
        public static bool IsInArcSector(Vector3 origin, Vector3 position, float angle)
            => Vector3.Dot(origin.normalized, position.normalized) > Mathf.Cos(angle * Mathf.Deg2Rad);
    }
}