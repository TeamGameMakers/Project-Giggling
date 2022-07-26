using UnityEngine;

namespace Data.Interactable
{
    [CreateAssetMenu(fileName = "Surveillance Camera Data", menuName = "Data/Surveillance Camera")]
    public class SurveillanceCamSO: ScriptableObject
    {
        public float rotationSpeed;
        public float minAngle;
        public float maxAngle;
    }
}