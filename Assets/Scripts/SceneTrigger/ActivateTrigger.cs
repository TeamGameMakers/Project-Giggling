using Save;
using UnityEngine;

namespace SceneTrigger
{
    public class ActivateTrigger : BaseSceneTrigger
    {
        // 是否检测激活
        public bool isActive;
        // 检测的 bool 值
        public string detectKey;
        [Tooltip("要处理的 GO, 如果为空则处理自身")]
        public GameObject handleObj;

        protected override void Start()
        {
            base.Start();
            HandleSelf();
        }

        protected override void TriggerEnterEvent(Collider2D col)
        {
            HandleSelf();
        }

        protected virtual void HandleSelf()
        {
            bool rec = SaveManager.GetBool(detectKey);
            Debug.Log("HandleSelf: " + rec);
            if (rec)
            {
                if (handleObj == null)
                    gameObject.SetActive(isActive);
                else
                    handleObj.SetActive(isActive);

                enabled = false;
            }
        }
    }
}
