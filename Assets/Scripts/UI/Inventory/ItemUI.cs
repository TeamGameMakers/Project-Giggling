// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;
//
// public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
// {
//     private Button btn;
//     public int itemIndex;
//     public Image image;
//
//     public Sprite ItemIcon { set { image.sprite = value; } }
//
//     [Header("Settings")]
//     public Sprite defaultImg;
//
//     public void Awake()
//     {
//         btn = GetComponent<Button>();
//         btn.onClick.AddListener(OnBtnClick);
//         image = GetComponent<Image>();
//         image.sprite = defaultImg;
//     }
//
//     public virtual void OnBtnClick()
//     {
//         AkSoundEngine.PostEvent("Play_UI_Common_Click", gameObject);
//
//         if (InventoryManager.Instance.selectedItem.image.sprite != image.sprite)
//         {
//             InventoryManager.Instance.selectedItem.ItemIcon = image.sprite;
//             InventoryManager.Instance.selectedItem.index = itemIndex;
//         }
//         else
//         {
//             InventoryManager.Instance.selectedItem.ItemIcon = defaultImg;
//             InventoryManager.Instance.selectedItem.index = -1;
//         }
//     }
//
//     public virtual void OnPointerEnter(PointerEventData eventData)
//     {
//         string info = GetComponentInParent<ItemSlotUI>().GetItemInfo();
//         InventoryManager.Instance.ShowInformation(transform.position, info);
//
//         AkSoundEngine.PostEvent("Play_UI_Common_Touch", gameObject);
//     }
//
//     public virtual void OnPointerExit(PointerEventData eventData)
//     {
//         InventoryManager.Instance.HideInformation();
//     }
// }
