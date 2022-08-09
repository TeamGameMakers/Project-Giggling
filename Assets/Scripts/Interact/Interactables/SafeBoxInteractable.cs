using System;
using System.Collections.Generic;
using Base.Event;
using Base.Resource;
using Data.Story;
using GM;
using Save;
using Story;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interact
{
    public class SafeBoxInteractable : Interactable
    {
        public new Camera camera;
        
        public GameObject pinLock;

        public string SaveKey => "SafeBoxUsed_" + GetInstanceID();

        protected string successEvent = "PinLockSuccessEvent";
        protected string failEvent = "PinLockFailEvent";

        [Header("电池生成设置")]
        [Tooltip("电池预制体")]
        public GameObject battery;
        [Tooltip("电池可能的生成位置")]
        public List<Transform> generatePosition;
        [Tooltip("电池数量")]
        public int num = 1;
        [Tooltip("收纳电池的场景物体")]
        public Transform generateRoot;
        
        // Boss 生成相关
        protected const string EventFirstBossEncounter = "FirstBossEncounter";

        protected override void Start()
        {
            base.Start();
            if (SaveManager.GetBool(SaveKey))
            {
                // 如果已经打开过则销毁自身
                DisableSelf();
            }
            else
            {
                pinLock.SetActive(false);
                // 限制 num
                num = Mathf.Clamp(num, 0, generatePosition.Count);
            }
        }

        public override void Interact(Interactor interactor)
        {
            // 先清空再绑定，确保只响应自己的事件
            EventCenter.Instance.RemoveAllListener(successEvent);
            EventCenter.Instance.RemoveAllListener(failEvent);
            EventCenter.Instance.AddEventListener(successEvent, Success);
            EventCenter.Instance.AddEventListener(failEvent, Fail);
            
            // 改成激活子物体
            pinLock.SetActive(true);
            GameManager.SwitchGameState(GameState.PinLock);
            // 移动到屏幕中心
            Vector3 mid = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            mid.z = 0;
            pinLock.transform.position = mid;
        }

        protected void Success()
        {
            Debug.Log("进入开锁成功事件");

            AkSoundEngine.PostEvent("Box_open", camera.gameObject);
            
            SaveManager.RegisterBool(SaveKey);
            Destroy(pinLock);
            //sr.sprite = finishSprite;

            // 在原地生成电池
            for (int i = 0; i < num; ++i)
            {
                GameObject btr = Instantiate(battery, generateRoot);
                btr.transform.position = generatePosition[i].position;
            }
            
            DisableSelf();
        }

        protected void Fail()
        {
            Debug.Log("进入开锁失败事件");
            
            AkSoundEngine.PostEvent("Box_defeat", camera.gameObject);
            
            SaveManager.RegisterBool(SaveKey);
            Destroy(pinLock);
            
            // 开锁失败，刷新 Boss
            // 如果是第一次就触发对话
            if (!SaveManager.GetBool(EventFirstBossEncounter))
            {
                StoryManager.Instance.StartStory(ResourceLoader.Load<PlotDataSO>("Data/Story/streetDialog_03"));
                SaveManager.RegisterBool(EventFirstBossEncounter);
            }
            // TODO: 创建 Boss
            //ResourceLoader.LoadAsync<GameObject>("Prefabs/");
            
            DisableSelf();
        }

        protected void DisableSelf()
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
