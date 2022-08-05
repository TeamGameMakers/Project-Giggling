// #if UNITY_EDITOR
// using UnityEngine;
// using UnityEditor;
//
// [CustomEditor(typeof(InventoryManager))]
//
// public class InventoryEdior : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//
//         // Create slot button
//         InventoryManager inventory = target as InventoryManager;
//         Transform parent = inventory.inventoryRT.transform;
//
//         if (GUILayout.Button("Create Slots"))
//         {
//             // Existent slots number
//             int childNum = parent.childCount;
//             Debug.Log(childNum);
//
//             // Clean existent slots
//             for (int i = 0; i < childNum; i++)
//             {
//                 DestroyImmediate(parent.GetChild(0).gameObject);
//             }
//             inventory.itemSlots.RemoveRange(0, childNum);
//
//             // Create new slots
//
//             for (int i = 0; i < inventory.slotsNum; i++)
//             {
//                 GameObject go = Instantiate(inventory.slotPrefab, parent);
//                 go.name = "ItemSlot " + i;
//                 go.GetComponent<ItemSlotUI>().index = i;
//                 inventory.itemSlots.Add(go.GetComponent<ItemSlotUI>());
//             }
//         }
//     }
// }
// #endif