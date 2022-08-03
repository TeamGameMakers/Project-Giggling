using System;
using Save;
using UnityEngine;

namespace SceneTrigger
{
    [AddComponentMenu("")]
    public abstract class BaseSceneTrigger : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("存档键")]
        protected string key;

        [SerializeField]
        [Tooltip("是否只触发一次")]
        protected bool triggerOnce = true;
        
        protected bool used;
        
        protected virtual void Start()
        {
            if (triggerOnce)
                used = SaveManager.GetBool(key);
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (!used && TriggerFilter(col))
            {
                Debug.Log("触发场景触发器：" + gameObject.name);

                TriggerEvent(col);

                if (triggerOnce)
                {
                    used = true;
                    SaveManager.RegisterBool(key);
                }
            }
        }

        protected abstract bool TriggerFilter(Collider2D col);
        
        protected abstract void TriggerEvent(Collider2D col);
    }
}
