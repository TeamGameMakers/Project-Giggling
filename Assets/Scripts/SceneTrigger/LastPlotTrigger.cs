using Base.Scene;
using GM;
using Save;
using Story;
using UI;
using UnityEngine;
using UnityEngine.Video;

namespace SceneTrigger
{
    public class LastPlotTrigger : StoryTrigger
    {
        public CGPlayer cgPlayer;
        protected VideoPlayer player;

        protected virtual void Awake()
        {
            player = cgPlayer.gameObject.GetComponent<VideoPlayer>();
        }

        protected override void StartStory()
        {
            StoryManager.Instance.StartStory(plot, FinishPlot);
            if (triggerOnce && used)
                Destroy(gameObject);
        }

        protected void FinishPlot()
        {
            Debug.Log("通关");
            // 记录通关
            SaveManager.GameClearRecord();
            
            player.loopPointReached += source => {
                RootCanvas.Instance.ShowAll();
                UIManager.Instance.ShowPanel<FaderPanel>("FaderPanel", "", UILayer.Top, panel => {
                    panel.fader.Alpha = 1;
                    SceneLoader.LoadScene("00_Phase_0");
                    panel.fader.Fade(0, f1 => {
                        UIManager.Instance.HidePanel("FaderPanel", true);
                    });
                });
            };
            
            RootCanvas.Instance.HideAll();
            GameManager.SwitchGameState(GameState.CG);
            //SaveManager.RegisterBool(cgPlayer.SaveKey);
            player.Play();
        }
    }
}
