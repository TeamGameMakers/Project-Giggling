using Save;
using UnityEngine;

namespace SceneTrigger
{
    public class SaveTrigger : BaseSceneTrigger
    {
        protected override void TriggerEnterEvent(Collider2D col)
        {
            SaveManager.Save();
        }
    }
}