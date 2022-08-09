using Data.Story;
using Story;
using UnityEngine;

namespace SceneTrigger
{
    public class StoryTrigger : BaseSceneTrigger
    {
        public PlotDataSO plot;

        protected override void TriggerEvent(Collider2D col)
        {
            StoryManager.Instance.StartStory(plot);
            if (triggerOnce && used)
            {
                Destroy(gameObject);
            }
        }
    }
}