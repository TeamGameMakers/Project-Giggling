using Data.Story;
using Story;
using UnityEngine;

namespace Interact
{
    public class StoryInteractable : Interactable
    {
        public PlotDataSO plot;
        
        public override void Interact(Interactor interactor)
        {
            StoryManager.Instance.StartStory(plot);
        }
    }
}
