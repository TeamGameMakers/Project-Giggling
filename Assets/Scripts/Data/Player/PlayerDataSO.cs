using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Player")]
        public float walkVelocity;
        public float runVelocity;
        public float maxHealthPoint;
        public float healthPoint;
        public float hpRestoreSpeed;
        
        [Header("Flash Light")]
        public bool hasFlashLight;
        public float powerUsingSpeed;
        public float lightRadius;
        public float lightAngle;
        public float lightDamage;
        public LayerMask layer;
    }
}
