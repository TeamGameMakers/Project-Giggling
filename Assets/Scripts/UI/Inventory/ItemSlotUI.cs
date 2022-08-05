// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;
//
// public class ItemSlotUI : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
// {
//     private Image slotImg;
//     public ItemUI itemUI;
//     public int index;
//
//     void Awake()
//     {
//         slotImg = GetComponent<Image>();
//         itemUI.itemIndex = index;
//     }
//
//     public void RefreshItemIcon(ItemData_SO itemData)
//     {
//         if (itemData != null)
//         {
//             itemUI.image.enabled = true;
//             itemUI.ItemIcon = itemData.itemIcon;
//         }
//         else
//         {
//             itemUI.ItemIcon = null;
//             itemUI.image.enabled = false;
//         }
//     }
//
//     public string GetItemInfo()
//     {
//         int i = InventoryManager.Instance.switchPage ? index + 6 : index;
//         Debug.Log(index);
//         ItemData_SO data = InventoryManager.Instance.inventoryData.itemDatas[i];
//         // 道具名字
//         return data.itemName;
//     }
// }
