using System;
using Base.Event;
using GM;
using Save;
using UnityEngine;

namespace Interact
{
    public class SafeBoxInteractable : Interactable
    {
        public Camera camera;
        
        public GameObject prefab;
        protected GameObject pinLock;

        public Sprite finishSprite;

        protected SpriteRenderer sr;
        protected Collider2D coll;
        
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
                sr.sprite = finishSprite;
                enabled = false;
            }
            else
            {
                EventCenter.Instance.AddEventListener(successEvent, Success);
                EventCenter.Instance.AddEventListener(failEvent, Fail);
            }
        }

        public override void Interact(Interactor interactor)
        {
            pinLock = Instantiate(prefab);
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

            DisableSelf();
            //sr.sprite = finishSprite;

            // TODO: 开锁成功出现其他物品
        }

        protected void Fail()
        {
            Debug.Log("进入开锁失败事件");
            
            Destroy(pinLock);
            
            DisableSelf();
            
            // TODO: 开锁失败，刷新 Boss
        }

        protected void DisableSelf()
        {
            gameObject.SetActive(false);
        }
    }
}
