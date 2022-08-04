// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// {
//     ItemUI item;
//     ItemSlotUI prevSlot;
//     RectTransform rectTrans;
//     // Vector2 prevAnchoredPos;
//     ItemSlotUI targetSlot;

//     void Awake()
//     {
//         item = GetComponent<ItemUI>();
//         prevSlot = GetComponentInParent<ItemSlotUI>();
//         rectTrans = GetComponent<RectTransform>();
//         // prevAnchoredPos = rectTrans.anchoredPosition;
//     }


//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         transform.SetParent(InventoryManager.Instance.dragCanvas.transform);
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         transform.position = eventData.position;
//     }

//     public void OnEndDrag(PointerEventData eventData)
//     {
//         var targetGameObject = eventData.pointerEnter;
//         ItemSlotUI targetSlot = null;

//         if (targetGameObject != null)
//         {
//             if (targetGameObject.CompareTag("Slot"))
//             {
//                 targetSlot = targetGameObject.GetComponent<ItemSlotUI>();
//                 InventoryManager.Instance.SwapItemData(prevSlot.index, targetSlot.index);
//             }
//             else if (targetGameObject.CompareTag("ItemIcon"))
//             {
//                 targetSlot = targetGameObject.GetComponentInParent<ItemSlotUI>();

//                 RectTransform targetRectTrans = targetGameObject.transform as RectTransform;
//                 targetRectTrans.SetParent(prevSlot.transform);
//                 targetGameObject.GetComponent<DragItem>().prevSlot = prevSlot;
//                 targetRectTrans.anchoredPosition = Vector2.zero;

//                 InventoryManager.Instance.SwapItemData(prevSlot.index, targetSlot.index);
//             }

//             if (targetSlot != null)
//                 prevSlot = targetSlot;
//             transform.SetParent(prevSlot.transform);
//         }
//         else
//         {
//             transform.SetParent(prevSlot.transform);
//         }

//         rectTrans.anchoredPosition = Vector2.zero;
//     }
// }
