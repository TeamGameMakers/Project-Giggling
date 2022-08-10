using Base.Event;
using Data;
using Save;
using UnityEngine;

namespace Interact
{
    public class LockDoorInteractable : Interactable
    {
        public string SaveKey => "unlock_" + gameObject.GetInstanceID();

        public SpriteRenderer doorRenderer;
        public Sprite openSprite;
        public Collider2D blockCollider;

        public int needKey;

        protected InventoryDataSO inventoryData;

        protected override void Start()
        {
            base.Start();

            if (SaveManager.GetBool(SaveKey))
                Open();
        }

        public override void Interact(Interactor interactor)
        {
            if (inventoryData == null)
                inventoryData = EventCenter.Instance.FuncTrigger<InventoryDataSO>("GetInventoryData");
            
            bool hasKey = false;
            switch (needKey)
            {
                case 1:
                    hasKey = inventoryData.hasKey1;
                    break;
                case 2:
                    hasKey = inventoryData.hasKey2;
                    break;
            }

            if (hasKey)
            {
                AkSoundEngine.PostEvent("Door_open", gameObject);
                Open();
                SaveManager.RegisterBool(SaveKey);
            }
            else
            {
                Debug.Log("调用关门音效");
                InputHandler.UseInteractInput();
                AkSoundEngine.PostEvent("Door_notopen", gameObject);
            }
        }

        protected virtual void Open()
        {
            doorRenderer.sprite = openSprite;
            blockCollider.enabled = false;
            // 自身交互关闭
            gameObject.SetActive(false);
        }
    }
}