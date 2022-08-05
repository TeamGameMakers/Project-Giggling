// using System.Collections.Generic;
// using Data;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace UI.Inventory
// {
//     public class InventoryPanel : BasePanel
//     {
//         [SerializeField] private InventoryDataSO _data;
//         private List<TextMeshProUGUI> _itemText;
//         private Image _itemImage;
//         private TextMeshProUGUI _itemName;
//         private TextMeshProUGUI _information;
//         private int _currentIndex;
//
//         protected override void Awake()
//         {
//             base.Awake();
//             
//             _itemText = new List<TextMeshProUGUI>();
//             
//             Init();
//         }
//
//         private void Init()
//         {
//             for (int index = 0; index < 6; index++)
//             {
//                 _itemText.Add(GetControl<TextMeshProUGUI>("ItemText " + index));
//                 _itemText[index].text = _data.items[index] ? _data.items[index].name : string.Empty;
//             }
//
//             _itemImage = GetControl<Image>("Item Sprite");
//             _itemName = GetControl<TextMeshProUGUI>("Name");
//             _information = GetControl<TextMeshProUGUI>("Information");
//
//             SelectItem(0);
//         }
//
//         public void SelectItem(int index)
//         {
//             _currentIndex = index;
//             
//             if (_data.items[index])
//             {
//                 _itemImage.sprite = _data.items[index].icon;
//                 _itemName.text = _data.items[index].name;
//                 _information.text = _data.items[index].info;
//                 _itemImage.enabled = true;
//                 _itemName.enabled = true;
//                 _information.enabled = true;
//             }
//             else
//             {
//                 _itemImage.enabled = false;
//                 _itemName.enabled = false;
//                 _information.enabled = false;
//             }
//         }
//
//         public void RefreshUI()
//         {
//             for (int index = 0; index < 6; index++)
//                 _itemText[index].text = _data.items[index] ? _data.items[index].name : string.Empty;
//
//             SelectItem(0);
//         }
//
//         public bool UseItem()
//         {
//             _data.items.Remove(_data.items[_currentIndex]);
//         }
//
//         public void DropItem()

//         {
//             
//         }
//     }
// }
