using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Data/Monster")]
    public class MonsterDataSo: ScriptableObject
    {
        [Header("Attribute")]
        public float walkSpeed;
        public float chaseSpeed;
        public int healthPoint;

        [Header("Detection")] 
        public float checkRadius;
        public float checkAngle;
        public LayerMask checkLayer;
    }
}