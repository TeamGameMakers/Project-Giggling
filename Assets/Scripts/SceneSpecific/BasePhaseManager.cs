using Base;
using Story;
using UI;
using UI.Inventory;
using UnityEngine;

namespace SceneSpecific
{
    public abstract class BasePhaseManager : SingletonMono<BasePhaseManager>
    {
        protected override void Start()
        {
            base.Start();

            UIManager.Instance.ShowPanel<GamePanel>("GamePanel");
            UIManager.Instance.ShowPanel<StatusPanel>("Status Panel");
            
            // 绑定音效物体
            StoryManager.Instance.RegisterAkObj(gameObject);
        }
    }
}
