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
            EventCenter.Instance.AddEventListener(eventName, Callback);
        }

        protected virtual void OnDisable()
        {
            EventCenter.Instance.RemoveEventListener(eventName, Callback);
        }

        protected virtual void Callback()
        {
            StoryManager.Instance.StartStory(plot);
        }
    }
}