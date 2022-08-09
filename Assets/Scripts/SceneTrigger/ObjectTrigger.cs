using System.Collections.Generic;
using UnityEngine;

namespace SceneTrigger
{
    public class ObjectTrigger : BaseSceneTrigger
    {
        public List<GameObject> objects = new List<GameObject>();

        protected override void TriggerEnterEvent(Collider2D col)
        {
            foreach (var obj in objects)
            {
                obj.SetActive(true);
            }
        }

        protected override void TriggerExitEvent(Collider2D col)
        {
            base.TriggerExitEvent(col);
            foreach (var obj in objects)
            {
                obj.SetActive(false);
            }
        }
    }
}
