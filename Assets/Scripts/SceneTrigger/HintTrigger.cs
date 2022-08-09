using UI;
using UnityEngine;

namespace SceneTrigger
{
    public class HintTrigger : BaseSceneTrigger
    {
        public string tip;

        protected override void TriggerEnterEvent(Collider2D col)
        {
            UIManager.Instance.ShowHint(tip);
        }

        protected override void TriggerExitEvent(Collider2D col)
        {
            UIManager.Instance.HideHint();
        }
    }
}
