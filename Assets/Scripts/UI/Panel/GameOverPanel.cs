using System;
using System.Collections.Generic;
using Base.Scene;
using Save;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverPanel : BasePanel
    {
        public List<Sprite> overSprites = new List<Sprite>();

        public float switchTime = 0.06f;

        protected Image image;

        protected float curTime = 0;
        protected int curIndex = 0;

        protected override void Awake()
        {
            base.Awake();
            image = GetControl<Image>("Image");
        }

        protected virtual void Update()
        {
            curTime += Time.deltaTime;

            if (curTime >= switchTime)
            {
                curTime = 0;
                ++curIndex;
                curIndex %= overSprites.Count;
                image.sprite = overSprites[curIndex];
            }
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "ContinueBtn":
                    AkSoundEngine.PostEvent("Menu_positive", gameObject);
                    // 覆盖当前 SaveData 内容
                    SaveManager.Load();
                    // 加载上一个存档
                    string sceneName = SaveManager.GetValue("SceneName");
                    if (string.IsNullOrEmpty(sceneName))
                        sceneName = SceneLoader.CurrentScene;
                    SceneLoader.LoadScene(sceneName);
                    // 关闭自身
                    UIManager.Instance.HidePanel("GameOverPanel", true);
                    break;
                case "QuitBtn":
                    AkSoundEngine.PostEvent("Menu_negative", gameObject);
                    UIManager.Instance.ShowPanel<FaderPanel>("FaderPanel", "", UILayer.System, panel => {
                        // 设置初始透明度
                        panel.fader.Alpha = 0;
                        
                        panel.fader.Fade(1, f => SceneLoader.LoadScene("00_Phase_0"));
                    });
                    break;
            }
        }
    }
}
