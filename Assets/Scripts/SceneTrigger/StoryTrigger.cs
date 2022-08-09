using Data.Story;
using Save;
using Story;
using UnityEngine;

namespace SceneTrigger
{
    public class StoryTrigger : BaseSceneTrigger
    {
        public PlotDataSO plot;
        

        protected override void TriggerEnterEvent(Collider2D col)
        {
            if (!SaveManager.GetBool(key))
            {
                StoryManager.Instance.StartStory(plot);
                if (triggerOnce && used)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}