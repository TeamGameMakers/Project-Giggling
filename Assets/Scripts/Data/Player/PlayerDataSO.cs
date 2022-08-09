using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerDataSO : ScriptableObject
    {
        public float walkVelocity;
        public float runVelocity;
        public float maxHealthPoint;
        public float healthPoint;
        public float hpRestoreSpeed;

        public bool hasFlashLight;
        public float powerUsingSpeed;
        public float lightRadius;
        public float lightAngle;
        public float lightDamage;
        public LayerMask layer;
    }
}
