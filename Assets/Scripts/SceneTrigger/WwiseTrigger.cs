using System;
using UnityEngine;

namespace SceneTrigger
{
    public class WwiseTrigger : BaseSceneTrigger
    {
        protected override void TriggerEnterEvent(Collider2D col)
        {
            Debug.Log("触发 Wwise");
        }
    }
}
