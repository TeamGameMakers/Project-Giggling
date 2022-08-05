// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;
//
// public class SwitchBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
// {
//     private Image image;
//     private bool sw;
//
//     public Sprite[] switchSprites = new Sprite[3];
//
//     private void Awake()
//     {
//         image = GetComponent<Image>();
//     }
//
//     public void OnPointerDown(PointerEventData eventData)
//     {
//         image.sprite = switchSprites[1];
//         AkSoundEngine.PostEvent("Play_UI_Common_Click", gameObject);
//     }
//
//     public void OnPointerUp(PointerEventData eventData)
//     {
//         image.sprite = sw ? switchSprites[0] : switchSprites[2];
//         InventoryManager.Instance.SwitchInventoryPage();
//         sw = !sw;
//     }
// }
