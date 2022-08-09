using System;
using Base.Event;
using Data;
using Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemInfoPanel : BasePanel
    {
        protected Button cgBtn;
        public Button CgBtn => cgBtn;
        
        protected ItemDataSO itemData;

        public event Action AfterPickEvent;

        protected override void Awake()
        {
            base.Awake();
            cgBtn = transform.Find("CloseBgBtn").GetComponent<Button>();
        }

        public void UpdateInfo(ItemDataSO data)
        {
            itemData = data;
            // 显示内容
            Image icon = GetControl<Image>("IconImage");
            icon.sprite = itemData.icon;
            TextMeshProUGUI title = GetControl<TextMeshProUGUI>("Title");
            title.SetText(data.name);
            TextMeshProUGUI content = GetControl<TextMeshProUGUI>("Content");
            content.SetText(data.info);

            Button pickUp = GetControl<Button>("PickUpBtn");

            if (data.canPick)
            {
                pickUp.gameObject.SetActive(true);
                pickUp.onClick.AddListener(PickUp);
            }
            else
                pickUp.gameObject.SetActive(false);
        }

        protected virtual void PickUp()
        {
            Debug.Log("拾取道具: " + itemData.name);

            // 关闭面板
            UIManager.Instance.HidePanel("ItemInfoPanel", true);
            
            AfterPickEvent?.Invoke();
        }
    }
}
