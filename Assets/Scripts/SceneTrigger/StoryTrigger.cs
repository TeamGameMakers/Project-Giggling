using Data.Story;
using Save;
using Story;
using UI;
using UnityEngine;

namespace SceneTrigger
{
    public class StoryTrigger : BaseSceneTrigger
    {
        public PlotDataSO plot;

        public float delay = 0;

        protected bool inTiming = false;
        protected float curTime = 0;

        protected bool inLoading = false;
        
        protected virtual void Update()
        {
            if (inTiming)
            {
                // 不在加载界面中时才计时
                if (inLoading)
                {
                    inLoading = UIManager.Instance.panelContainer.ContainsKey("LoadingPanel");
                    return;
                }

                curTime += Time.deltaTime;
                
                if (curTime >= delay)
                {
                    inTiming = false;
                    StartStory();
                }
            }
        }

        protected override void TriggerEnterEvent(Collider2D col)
        {
            if (!SaveManager.GetBool(key))
            {
                // 如果要就计时
                if (delay != 0)
                {
                    inTiming = true;
                    // 检查是否在加载界面
                    inLoading = UIManager.Instance.panelContainer.ContainsKey("LoadingPanel");
                }
                else
                {
                    StartStory();
                }
            }
        }

        protected virtual void StartStory()
        {
            StoryManager.Instance.StartStory(plot);
            if (triggerOnce && used)
                Destroy(gameObject);
        }
    }
}