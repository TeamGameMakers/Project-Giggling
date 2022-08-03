// using System;
// using Save;
// using UnityEngine;
//
// namespace SceneTrigger
// {
//     [AddComponentMenu("")]
//     public abstract class BaseSceneTrigger : MonoBehaviour
//     {
//         [SerializeField]
//         [Tooltip("存档键")]
//         protected string key;
//
//         [SerializeField]
//         [Tooltip("是否只触发一次")]
//         protected bool triggerOnce = true;
//         
//         protected bool used;
//         
//         protected virtual void Start()
//         {
//             if (triggerOnce)
//                 used = SaveManager.GetBool(key);
//         }
//
//         protected virtual void OnTriggerEnter2D(Collider2D col)
//         {
//             if (!used && col.CompareTag("Player"))
//             {
//                 Debug.Log("触发场景触发器：" + gameObject.name);
//
//                 TriggerEvent();
//
//                 if (triggerOnce)
//                 {
//                     used = true;
//                     SaveManager.RegisterBool(key);
//                 }
//             }
//         }
//
//         protected abstract void TriggerEvent();
//     }
// }
