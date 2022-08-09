using System;
using Base.Event;
using UnityEngine;

namespace SceneTrigger
{
    public class WwiseTrigger : BaseSceneTrigger
    {
        public string wwiseEvent;

        [Tooltip("同时触发的事件，可以置空")]
        public string triggerEvent;

        protected override void TriggerEnterEvent(Collider2D col)
        {
            Debug.Log("触发 Wwise");
            AkSoundEngine.PostEvent(wwiseEvent, gameObject);
            
            if (!string.IsNullOrEmpty(triggerEvent))
                EventCenter.Instance.EventTrigger(triggerEvent);
        }
    }
}
