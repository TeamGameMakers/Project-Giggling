using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        public float moveVelocity;
    }
}
