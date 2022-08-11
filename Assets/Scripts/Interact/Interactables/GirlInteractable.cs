using System.Collections;
using Data.Story;
using Save;
using Story;
using UI;
using UnityEngine;

namespace Interact
{
    public class GirlInteractable : StoryInteractable
    {
        public PlotDataSO afterPlot;

        public float waitSecond = 2;
        
        protected new SpriteRenderer renderer;
        protected Collider2D coll;

        protected override void Awake()
        {
            base.Awake();
            renderer = GetComponent<SpriteRenderer>();
            coll = GetComponent<Collider2D>();
        }

        protected override void Start()
        {
            base.Start();
            // 读取存档
            if (SaveManager.GetBool("MeetGirl"))
            {
                renderer.enabled = false;
                coll.enabled = false;
            }
        }

        public override void Interact(Interactor interactor)
        {
            StoryManager.Instance.StartStory(plot, FinishPlot);
            if (triggerOnce)
            {
                SaveManager.RegisterBool(SaveKey);
                DisableSelf();
            }
        }

        protected void FinishPlot()
        {
            UIManager.Instance.ShowPanel<FaderPanel>("FaderPanel", "", UILayer.Top, panel => {
                panel.fader.Alpha = 0;
                StartCoroutine(FaderCoroutine(panel));
                // 注册遇见女主
                SaveManager.RegisterBool("MeetGirl");
            });
        }

        protected IEnumerator FaderCoroutine(FaderPanel panel)
        {
            yield return panel.fader.FadeCoroutine(1);

            renderer.enabled = false;
            coll.enabled = false;
            
            yield return new WaitForSeconds(waitSecond);
            
            yield return panel.fader.FadeCoroutine(0);
            UIManager.Instance.HidePanel("FaderPanel", true);
            
            AkSoundEngine.PostEvent("School_indoorFcrazy", gameObject);
            StoryManager.Instance.StartStory(afterPlot);
        }
    }
}
