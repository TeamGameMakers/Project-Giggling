using Story;
using UnityEngine.Playables;

namespace SceneTrigger
{
    public class StoryTimelineTrigger : StoryTrigger
    {
        public PlayableDirector director;
        
        protected override void StartStory()
        {
            StoryManager.Instance.StartStory(plot, FinishPlot);
            if (triggerOnce && used)
                Destroy(gameObject);
        }

        protected virtual void FinishPlot()
        {
            director.Play();
        }
    }
}