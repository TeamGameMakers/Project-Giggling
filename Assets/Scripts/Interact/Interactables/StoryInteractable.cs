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

        protected Collider2D coll;
        protected Interactable itrt;

        protected override void Awake()
        {
            base.Awake();
            coll = GetComponent<Collider2D>();
            itrt = GetComponent<Interactable>();
        }

        protected override void Start()
        {
            base.Start();
            if (triggerOnce && SaveManager.GetBool(SaveKey))
            {
                DisableSelf();
            }
        }

        public override void Interact(Interactor interactor)
        {
            StoryManager.Instance.StartStory(plot);
            if (triggerOnce)
            {
                SaveManager.RegisterBool(SaveKey);
                DisableSelf();
            }
        }

        protected virtual void DisableSelf()
        {
            coll.enabled = false;
            itrt.enabled = false;
        }
    }
}
