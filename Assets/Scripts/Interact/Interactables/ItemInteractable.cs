using Data;
using Save;
using Story;
using UI;
using UnityEngine;

namespace Interact
{
    public class ItemInteractable : Interactable
    {
        public ItemDataSO item;

        public string SaveKey => "pick_item_" + gameObject.GetInstanceID();
        
        protected override void Start()
        {
            base.Start();
            // 道具已经被拾取了
            // 记录拾取，需要在 UI 或背包中实现
            if (SaveManager.GetBool(SaveKey))
            {
                Destroy(gameObject);
            }
        }

        public override void Interact(Interactor interactor)
        {
            Debug.Log("与道具交互");
            UIManager.Instance.ShowPanel<ItemInfoPanel>("ItemInfoPanel", callBack: panel => {
                panel.UpdateInfo(item, SaveKey);

                panel.AfterPickEvent += () => {
                    // 销毁自己
                    Destroy(gameObject);
                };
                
                if (item.firstPlot != null && !SaveManager.GetBool(item.FirstSaveKey))
                {
                    panel.AfterPickEvent += () => {
                        // 记录第一次拾取
                        SaveManager.RegisterBool(item.FirstSaveKey);
                        // 开始剧情
                        StoryManager.Instance.StartStory(item.firstPlot);
                    };
                }
            });
        }
    }
}
