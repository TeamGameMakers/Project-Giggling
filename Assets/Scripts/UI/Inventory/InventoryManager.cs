// using System.Collections;
// using System.Collections.Generic;
// using Data;
// using UI.Inventory;
// using UnityEngine;
//
// public class InventoryManager : Singleton<InventoryManager>
// {
//     private RectTransform rectTrans;
//     private Vector2 anchoredPos;
//     private bool moveStopped;
//     private Vector2 showPos;
//     private int switchNum = 6;
//     public bool switchPage;
//
//     [Header("Inventory Settings")]
//     public RectTransform inventoryRT;
//     public Vector2 hidePos;
//     public float showUpSpeed;
//     public InventoryDataSO inventoryData;
//     public InventoryDataSO usedItemData;
//     public SelectedItem selectedItem;
//     public bool isOpen;
//
//     [Header("Slots Settings")]
//     public GameObject slotPrefab;
//     public int slotsNum = 6;
//     public List<ItemSlotUI> itemSlots;
//
//     [Header("Information Settings")]
//     public InformationUI informationUI;
//     public Vector2 posOffset;
//     [SceneName][SerializeField] private string uiHiddenScene;
//     [SerializeField] private GameObject[] toHideGameObjects = { };
//     protected override void Awake()
//     {
//         base.Awake();
//         rectTrans = GetComponent<RectTransform>();
//     }
//
//     void Start()
//     {
//         inventoryData = SaveManager.Instance.inventoryData;
//         usedItemData = SaveManager.Instance.usedItemData;
//         InventoryInit();
//         HideInformation();
//         if (SceneLoadManager.Instance)
//         {
//             SceneLoadManager.Instance.afterSceneLoadedActions.AddListener(this.ShowUIByScene);
//         }
//     }
//
//     void OnDisable()
//     {
//         if (SceneLoadManager.Instance)
//         {
//             SceneLoadManager.Instance.afterSceneLoadedActions.RemoveListener(this.ShowUIByScene);
//         }
//     }
//
//     void Update()
//     {
//         inventoryRT.anchoredPosition = anchoredPos;
//
//         if (SceneLoadManager.Instance.CurrentScene == "StartScene" && isOpen)
//             CloseInventory();
//     }
//
//     #region UI Interact
//
//     public void InventoryInit()
//     {
//         anchoredPos = inventoryRT.anchoredPosition;
//         showPos = anchoredPos;
//         anchoredPos = hidePos;
//         moveStopped = true;
//
//         RefreshUI();
//     }
//
//     public void OpenInventory()
//     {
//         RefreshUI();
//         if (moveStopped)
//         {
//             StartCoroutine(UIMovement(showPos));
//             AkSoundEngine.PostEvent("Play_UI_Item_Window_Open", gameObject);
//         }
//
//         isOpen = true;
//     }
//
//     public void CloseInventory()
//     {
//         if (moveStopped)
//         {
//             StartCoroutine(UIMovement(hidePos));
//             AkSoundEngine.PostEvent("Play_UI_Item_Window_Close", gameObject);
//         }
//
//         isOpen = false;
//     }
//
//     IEnumerator UIMovement(Vector2 destPos)
//     {
//         moveStopped = false;
//
//         while ((anchoredPos - destPos).sqrMagnitude > 0.5f)
//         {
//             anchoredPos = Vector3.Lerp(anchoredPos, destPos, showUpSpeed * Time.deltaTime);
//             yield return null;
//         }
//
//         moveStopped = true;
//     }
//
//     public void ShowInformation(Vector2 slotPos, string info)
//     {
//         informationUI.gameObject.SetActive(true);
//         informationUI.transform.position = slotPos + posOffset;
//         informationUI.SetInformation(info);
//     }
//
//     public void HideInformation()
//     {
//         informationUI.gameObject.SetActive(false);
//     }
//
//     public void SwitchInventoryPage()
//     {
//         switchPage = !switchPage;
//         RefreshUI();
//     }
//
//     #endregion
//
//     #region Inventory Data
//
//     public void AddItem(ItemData_SO itemData)
//     {
//
//         if (inventoryData.itemDatas.Count < 12)
//             inventoryData.itemDatas.Add(itemData);
//         // TODO 最好再有一层Item对象来解耦
//         // 触发警戒值
//         if (VrtraChasingSystem.Instance)
//         {
//             VrtraChasingSystem.Instance.ai.AddAngryVal(itemData.vrtraAngValForCollect);
//             VrtraChasingSystem.Instance.ai.VrtraActOnce();
//         }
//         else
//             Debug.Log("inventory full!");
//
//         RefreshUI();
//     }
//
//     private void RemoveItem(ItemData_SO itemData)
//     {
//         int index = inventoryData.itemDatas.IndexOf(itemData);
//         usedItemData.itemDatas.Add(itemData);
//         bool check = inventoryData.itemDatas.Remove(itemData);
//         if (!check)
//         {
//             Debug.Log("你没有 " + itemData.name);
//             return;
//         }
//         itemSlots[index].RefreshItemIcon(null);
//         selectedItem.ItemIcon = selectedItem.defaultImg;
//         RefreshUI();
//     }
//
//     public bool HasItem(ItemData_SO itemData)
//     {
//         return inventoryData.itemDatas.Contains(itemData) || usedItemData.itemDatas.Contains(itemData);
//     }
//
//     public void SwapItemData(int index1, int index2)
//     {
//         ItemData_SO tmpData = inventoryData.itemDatas[index1];
//         inventoryData.itemDatas[index1] = inventoryData.itemDatas[index2];
//         inventoryData.itemDatas[index2] = tmpData;
//     }
//
//     /// <summary>
//     /// 使用道具
//     /// </summary>
//     /// <param name="name">需要的道具名称</param>
//     /// <returns>true: 使用成功; false: 没有所需道具</returns>
//     public bool UseItem(string name)
//     {
//         if (name == "点击开锁")
//             return true;
//
//         // 空手
//         if (selectedItem.index == -1)
//         {
//             Debug.Log("空手");
//             return false;
//         }
//
//         if (inventoryData.itemDatas[selectedItem.index].itemName == name)
//         {
//             if (!inventoryData.itemDatas[selectedItem.index].notConsumable)
//             {
//                 // TODO 最好再有一层Item对象来解耦
//                 // 触发警戒值
//                 if (VrtraChasingSystem.Instance)
//                 {
//                     VrtraChasingSystem.Instance.ai.AddAngryVal(inventoryData.itemDatas[selectedItem.index].vrtraAngValForUse);
//                     this.vrtraInterupt = false;
//                     VrtraChasingSystem.Instance.BindOnVrtraPosChangedEvent(this.OnVrtraCatch);
//                     VrtraChasingSystem.Instance.ai.VrtraActOnce();
//                     VrtraChasingSystem.Instance.UnbindOnVrtraPosChangedEvent(this.OnVrtraCatch);
//                     if (this.vrtraInterupt){
//                         return false;
//                     }
//                 }
//                 RemoveItem(inventoryData.itemDatas[selectedItem.index]);
//                 selectedItem.index = -1;
//             }
//             Debug.Log("使用物品");
//             return true;
//         }
//         else
//         {
//             Debug.Log("没什么作用");
//             return false;
//         }
//     }
//
//     private bool vrtraInterupt = false;
//     public void OnVrtraCatch(){
//         this.vrtraInterupt = true;
//     }
//
//     public void RefreshUI()
//     {
//         // 清空 slots
//         for (int i = 0; i < 6; i++)
//         {
//             itemSlots[i].RefreshItemIcon(null);
//         }
//
//         int startIndex = switchPage ? switchNum : 0;
//         int clamp;
//         if (inventoryData.itemDatas.Count > switchNum)
//         {
//             clamp = switchPage ? inventoryData.itemDatas.Count : switchNum;
//         }
//         else
//             clamp = inventoryData.itemDatas.Count;
//
//         // Debug.Log(clamp + " " + startIndex);
//
//         // 更新 slots
//         for (int i = startIndex; i < clamp; i++)
//         {
//             itemSlots[i - startIndex].RefreshItemIcon(inventoryData.itemDatas[i]);
//         }
//     }
//
//     #endregion
//
//     public void ShowUIByScene()
//     {
//         if (!SceneLoadManager.Instance)
//         {
//             return;
//         }
//         foreach (GameObject go in this.toHideGameObjects)
//         {
//             go.SetActive(SceneLoadManager.Instance.CurrentScene != this.uiHiddenScene);
//         }
//     }
// }
