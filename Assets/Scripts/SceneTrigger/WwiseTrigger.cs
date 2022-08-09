using System;
using Base.Event;
using UnityEngine;

namespace SceneTrigger
{
    public class WwiseTrigger : BaseSceneTrigger
    {
        public string wwiseEvent;

        public string actionEvent;

        protected override void TriggerEnterEvent(Collider2D col)
        {
            Debug.Log("触发 Wwise");
            AkSoundEngine.PostEvent(wwiseEvent, gameObject);
            
            if (!string.IsNullOrEmpty(actionEvent))
                EventCenter.Instance.EventTrigger(actionEvent);
        }
    }
}
