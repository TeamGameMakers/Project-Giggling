using System;
using Save;
using UnityEngine;

namespace SceneTrigger
{
    [AddComponentMenu("")]
    public abstract class BaseSceneTrigger : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("存档键，如果不是只触发一次则无视")]
        protected string key;

        [SerializeField]
        [Tooltip("是否只触发一次")]
        protected bool triggerOnce = true;
        
        protected bool used;
        protected bool enter;
        
        protected virtual void Start()
        {
            if (triggerOnce)
            {
                used = SaveManager.GetBool(key);
                if (used)
                    Destroy(gameObject);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (!used && TriggerFilter(col))
            {
                Debug.Log("触发场景触发器：" + gameObject.name);
                enter = true;

                TriggerEnterEvent(col);
                
                if (triggerOnce)
                {
                    used = true;
                    SaveManager.RegisterBool(key);
                }
            }
        }

        protected virtual bool TriggerFilter(Collider2D col)
        {
            return col.CompareTag("Player");
        }
        
        protected abstract void TriggerEnterEvent(Collider2D col);

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (enter && TriggerFilter(other))
            {
                enter = false;
                TriggerExitEvent(other);
            }
        }

        protected virtual void TriggerExitEvent(Collider2D col)
        {
            
        }
    }
}
