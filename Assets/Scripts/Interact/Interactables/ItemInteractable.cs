// using Data;
// using Save;
// using UI;
// using UnityEngine;
//
// namespace Interact
// {
//     public class ItemInteractable : Interactable
//     {
//         public ItemDataSO item;
//         public string SaveKey => "pick_item_" + item.name;
//         
//         protected override void Start()
//         {
//             base.Start();
//             // 道具已经被拾取了
//             if (SaveManager.GetBool(SaveKey))
//             {
//                 gameObject.SetActive(false);
//             }
//         }
//
//         public override void Interact(Interactor interactor)
//         {
//             Debug.Log("与道具交互");
//             UIManager.Instance.ShowPanel<ItemInfoPanel>("ItemInfoPanel", callBack: panel => panel.UpdateInfo(item));
//         }
//     }
// }
