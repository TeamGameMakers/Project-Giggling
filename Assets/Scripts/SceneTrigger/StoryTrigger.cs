using Data.Story;
using Story;
using UnityEngine;

namespace SceneTrigger
{
    public class StoryTrigger : BaseSceneTrigger
    {
        public PlotDataSO plot;

        protected override bool TriggerFilter(Collider2D col)
        {
            return col.CompareTag("Player");
        }

        protected override void TriggerEvent(Collider2D col)
        {
            StoryManager.Instance.StartStory(plot);
        }
    }
}