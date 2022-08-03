using UnityEngine;
using UnityEngine.Events;

namespace SceneTrigger
{
    public class ActionTrigger : BaseSceneTrigger
    {
        public UnityAction<Collider2D> actions;

        public LayerMask layerMask;

        public string tagMask;

        protected override bool TriggerFilter(Collider2D col)
        {
            return col.IsTouchingLayers(layerMask) && col.CompareTag(tagMask);
        }

        protected override void TriggerEvent(Collider2D col)
        {
            actions?.Invoke(col);
        }
    }
}