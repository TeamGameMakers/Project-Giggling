using System;
using Data.Story;
using Save;
using Story;
using UnityEngine;

namespace SceneTrigger
{
    public class StoryTrigger : BaseSceneTrigger
    {
        public PlotDataSO plot;

        public float delay = 0;

        protected bool inTiming = false;
        protected float curTime = 0;
        
        protected virtual void Update()
        {
            if (inTiming)
            {
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
                }
                else
                {
                    StartStory();
                }
            }
        }

        protected void StartStory()
        {
            StoryManager.Instance.StartStory(plot);
            if (triggerOnce && used)
                Destroy(gameObject);
        }
    }
}