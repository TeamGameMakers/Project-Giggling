using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Security Camera Data", menuName = "Data/Security Camera")]
    public class SecurityCameraSO: ScriptableObject
    {
        public float rotationSpeed;
        public float minAngle;
        public float maxAngle;
    }
}