using UnityEngine;

namespace Data
{
    public class InteractableDataSO: ScriptableObject
    {
        [Header("Interactable")]
        public Sprite defaultSprite;
        public Sprite highLightSprite;
        public float checkRadius;
        public LayerMask checkLayer;
    }
}