using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemInfoPanel : BasePanel
    {
        protected ItemDataSO itemData;
        
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
            // TODO: 加入背包
            pickUp.onClick.AddListener(null);
            if (data.canPick)
                pickUp.gameObject.SetActive(true);
            else
                pickUp.gameObject.SetActive(false);
        }
    }
}
