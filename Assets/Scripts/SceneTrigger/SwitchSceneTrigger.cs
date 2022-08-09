using System.Collections;
using System.Collections.Generic;
using Base.Mono;
using Base.Scene;
using UI;
using UnityEngine;

namespace SceneTrigger
{
    public class SwitchSceneTrigger : BaseSceneTrigger
    {
        public string toScene;

        public float fadeDuration = 0.5f;

        protected FaderPanel fp;

        [Tooltip("需要在切换场景前销毁的物品")]
        public List<GameObject> destroyObjs = new List<GameObject>();
        
        protected override void TriggerEnterEvent(Collider2D col)
        {
            UIManager.Instance.ShowPanel<FaderPanel>("FaderPanel", "", UILayer.System, panel => {
                fp = panel;
                // 设置初始透明度
                panel.fader.Alpha = 0;
                panel.fader.fadeDuration = fadeDuration;

                MonoManager.Instance.StartCoroutine(SwitchCoroutine());
            });
        }

        protected virtual IEnumerator SwitchCoroutine()
        {
            // 渐黑
            yield return fp.fader.FadeCoroutine(1);
            for (int i = destroyObjs.Count - 1; i >= 0; --i)
            {
                Destroy(destroyObjs[i]);
            }

            yield return SceneLoader.LoadSceneAsyncCoroutine(toScene, null);

            // 渐隐
            yield return fp.fader.FadeCoroutine(0, f => UIManager.Instance.HidePanel("FaderPanel", true));
        }
    }
}
