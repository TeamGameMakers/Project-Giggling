using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerDataSO : ScriptableObject
    {
        public float moveVelocity;
    }
}
