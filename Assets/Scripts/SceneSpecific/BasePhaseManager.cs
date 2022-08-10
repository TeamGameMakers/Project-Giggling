using System;
using System.Collections.Generic;
using Base;
using Base.Event;
using Characters.Player;
using Save;
using Story;
using UI;
using UI.Inventory;
using UnityEngine;

namespace SceneSpecific
{
    public abstract class BasePhaseManager<T> : SingletonMono<T> where T : MonoBehaviour
    {
        public Transform player;

        public List<string> fromScenes = new List<string>();

        public List<Transform> enterPoints = new List<Transform>();

        protected override void Start()
        {
            base.Start();

            UIManager.Instance.ShowPanel<GamePanel>("GamePanel");
            UIManager.Instance.ShowPanel<StatusPanel>("Status Panel");
            
            // 绑定音效物体
            StoryManager.Instance.RegisterAkObj(gameObject);
            
            // 如果是切换场景，则设置位置
            string fromScene = SaveManager.GetFromScene();
            if (!string.IsNullOrEmpty(fromScene))
            {
                for (int i = 0; i < fromScenes.Count; ++i)
                {
                    if (fromScene == fromScenes[i])
                    {
                        player.position = enterPoints[i].position;
                        break;
                    }
                }
            }
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
