using Base.Event;
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

        [Tooltip("是否检查能否拾取")]
        public bool checkPickable = false;
        
        [Tooltip("拾取道具后的事件，为空则不执行")]
        public string afterPickEvent = "";
        
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
                panel.UpdateInfo(item);

                panel.AfterPickEvent += () => {
                    if (!string.IsNullOrEmpty(afterPickEvent))
                    {
                        if (checkPickable)
                        {
                            // 成功拾取，才销毁物品
                            if (EventCenter.Instance.FuncTrigger<int, bool>(afterPickEvent, 1))
                            {
                                // 销毁自己
                                Destroy(gameObject);
                                // 记录拾取道具
                                SaveManager.RegisterBool(SaveKey);
                            }
                            else
                                Debug.Log("拾取失败");
                        }
                        else
                        {
                            EventCenter.Instance.EventTrigger(afterPickEvent);
                            // 销毁自己
                            Destroy(gameObject);
                            // 记录拾取道具
                            SaveManager.RegisterBool(SaveKey);
                        } 
                    }
                };
                
                // 检查是否有第一次拾取触发剧情，由于是第一次拾取，所以不用检查能否拾取
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
