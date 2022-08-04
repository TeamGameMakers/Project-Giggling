using System;
using Base.Event;
using Data.Story;
using UnityEngine;

namespace Story
{
    public class StoryEventRegister : MonoBehaviour
    {
        public PlotDataSO plot;
        public string eventName;

        protected virtual void Start()
        {
            EventCenter.Instance.AddEventListener(eventName, () => StoryManager.Instance.StartStory(plot));
        }
    }
}