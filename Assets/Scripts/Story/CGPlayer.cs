using System;
using Base.Event;
using GM;
using Save;
using UI;
using UnityEngine;
using UnityEngine.Video;

namespace Story
{
    public class CGPlayer : MonoBehaviour
    {
        public string SaveKey => "cg_" + gameObject.name;
        public string plotEvent;

        public string wwiseEvent;
        public string wwiseStopEvent;
        
        private VideoPlayer m_player;
        
        private void Awake()
        {
            m_player = GetComponent<VideoPlayer>();
        }

        private void Start()
        {
            if (SaveManager.GetBool(SaveKey))
            {
                gameObject.SetActive(false);
            }
            else
            {
                m_player.started += source => {
                    if (!string.IsNullOrEmpty(wwiseEvent))
                        AkSoundEngine.PostEvent(wwiseEvent, gameObject);
                }; 
                    
                m_player.loopPointReached += source => {
                    GameManager.BackGameState();
                    SaveManager.RegisterBool(SaveKey);
                    if (!string.IsNullOrEmpty(wwiseStopEvent))
                        AkSoundEngine.PostEvent(wwiseStopEvent, gameObject);
                    if (string.IsNullOrEmpty(plotEvent))
                        gameObject.SetActive(false);
                };
                if (!string.IsNullOrEmpty(plotEvent))
                {
                    m_player.loopPointReached += source => {
                        EventCenter.Instance.EventTrigger(plotEvent);
                        gameObject.SetActive(false);
                    };
                }
            }
        }
    }
}
