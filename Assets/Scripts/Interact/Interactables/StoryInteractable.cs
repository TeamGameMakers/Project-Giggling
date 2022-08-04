using Data.Story;
using Save;
using Story;
using UnityEngine;

namespace Interact
{
    public class StoryInteractable : Interactable
    {
        public PlotDataSO plot;

        public string SaveKey => "pick_story_" + plot.name;

        public bool triggerOnce = true;

        protected override void Start()
        {
            base.Start();
            if (triggerOnce && SaveManager.GetBool(SaveKey))
            {
                gameObject.SetActive(false);
            }
        }

        public override void Interact(Interactor interactor)
        {
            StoryManager.Instance.StartStory(plot);
            if (triggerOnce)
            {
                SaveManager.RegisterBool(SaveKey);
                gameObject.SetActive(false);
            }
        }
    }
}
