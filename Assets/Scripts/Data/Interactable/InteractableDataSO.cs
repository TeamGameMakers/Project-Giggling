using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "InteractableData", menuName = "Data/Interactable Data")]
    public class InteractableDataSO: ScriptableObject
    {
        [Header("Interactable")]
        public float checkRadius;
        public LayerMask checkLayer;
    }
}