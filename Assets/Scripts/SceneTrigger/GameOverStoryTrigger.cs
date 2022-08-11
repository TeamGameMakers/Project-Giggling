using Story;
using UI;

namespace SceneTrigger
{
    public class GameOverStoryTrigger : StoryTrigger
    {
        protected override void StartStory()
        {
            StoryManager.Instance.StartStory(plot, () => UIManager.Instance.ShowGameOverPanel());
            if (triggerOnce && used)
                Destroy(gameObject);
        }
    }
}