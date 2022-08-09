using System;
using Base;
using Base.Event;
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

        private void OnEnable()
        {
            EventCenter.Instance.AddEventListener("GameOver", GameOver);
        }

        private void OnDisable()
        {
            EventCenter.Instance.RemoveEventListener("GameOver", GameOver);
        }

        private void GameOver()
        {
            UIManager.Instance.ShowGameOverPanel();
        }
    }
}
