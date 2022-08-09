using UnityEngine;

namespace SceneTrigger
{
    public abstract class CheckSceneTrigger : BaseSceneTrigger
    {
        protected override void TriggerEnterEvent(Collider2D col)
        {
            if (CheckTrigger())
                PassCheck();
            else
                NotPassCheck();
        }

        protected abstract bool CheckTrigger();

        protected virtual void PassCheck()
        {
            Debug.Log("通过场景检查触发器");
        }

        protected virtual void NotPassCheck()
        {
            Debug.Log("未通过场景检查触发器");
        }
    }
}
