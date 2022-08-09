using System;
using Base.Event;
using GM;
using Save;
using UnityEngine;

namespace Interact
{
    public class SafeBoxInteractable : Interactable
    {
        public new Camera camera;
        
        public GameObject pinLock;

        public Sprite finishSprite;

        protected SpriteRenderer sr;
        protected Collider2D coll;

        public GameObject successObj;
        
        public string saveKey;

        protected string successEvent = "PinLockSuccessEvent";
        protected string failEvent = "PinLockFailEvent";

        protected override void Awake()
        {
            base.Awake();
            sr = GetComponent<SpriteRenderer>();
            coll = GetComponent<Collider2D>();
        }

        protected override void Start()
        {
            base.Start();
            if (SaveManager.GetBool(saveKey))
            {
                // sr.sprite = finishSprite;
                // enabled = false;
                
                // 如果已经打开过则销毁自身
                DisableSelf();
            }
            else
            {
                pinLock.SetActive(false);
                EventCenter.Instance.AddEventListener(successEvent, Success);
                EventCenter.Instance.AddEventListener(failEvent, Fail);
            }
        }

        public override void Interact(Interactor interactor)
        {
            // 改成激活子物体
            pinLock.SetActive(true);
            GameManager.SwitchGameState(GameState.PinLock);
            // 移动到屏幕中心
            Vector3 mid = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            mid.z = 0;
            pinLock.transform.position = mid;
        }

        protected virtual void OnDisable()
        {
            EventCenter.Instance.RemoveEventListener(successEvent, Success);
            EventCenter.Instance.RemoveEventListener(failEvent, Fail);
        }

        protected void Success()
        {
            Debug.Log("进入开锁成功事件");
            SaveManager.RegisterBool(saveKey);
            Destroy(pinLock);
            //sr.sprite = finishSprite;

            // 开锁成功出现其他物品
            successObj.SetActive(true);
            
            DisableSelf();
        }

        protected void Fail()
        {
            Debug.Log("进入开锁失败事件");
            Destroy(pinLock);
            
            // TODO: 开锁失败，刷新 Boss
            
            DisableSelf();
        }

        protected void DisableSelf()
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
