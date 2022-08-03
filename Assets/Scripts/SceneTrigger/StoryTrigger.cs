using Data.Story;
using Story;

namespace SceneTrigger
{
    public class StoryTrigger : BaseSceneTrigger
    {
        public PlotDataSO plot;
        
        protected override void TriggerEvent()
        {
            StoryManager.Instance.StartStory(plot);
        }
    }
}