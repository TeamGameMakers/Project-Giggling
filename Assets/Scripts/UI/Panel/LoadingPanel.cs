using System;
using System.Collections;
using Base.Scene;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class LoadingPanel : BasePanel
    {
        protected bool ready = false;
        protected bool canClose = false;
        protected bool canFade = false;

        protected TextMeshProUGUI tmp;
        protected CanvasGroupFader panelFader;
        protected CanvasGroupFader contentFader;

        public bool waitInput = false;

        protected override void Start()
        {
            base.Start();

            panelFader = GetComponent<CanvasGroupFader>();
            contentFader = transform.Find("ContainerPanel").GetComponent<CanvasGroupFader>();
            // 设置文字
            tmp = GetControl<TextMeshProUGUI>("Content");
            tmp.SetText(tmp.text + "  随机选择内容");

            ready = true;
        }

        protected virtual void Update()
        {
            // 检查任意键输入
            if (canClose && InputHandler.AnyKeyPressed)
            {
                // 开始渐隐
                canFade = true;
                canClose = false;
            }
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName));
        }

        protected IEnumerator LoadSceneCoroutine(string sceneName)
        {
            // 等待准备完成
            while (!ready)
            {
                yield return null;
            }
            yield return panelFader.FadeCoroutine(1);
            // 加载场景
            yield return SceneLoader.SwitchSceneLoadCoroutine(sceneName);

            if (waitInput)
            {
                // 等待切换
                canClose = true;
                tmp.SetText("按任意键继续");
                while (!canFade)
                {
                    yield return null;
                }
                yield return contentFader.FadeCoroutine(0);
            }
            
            // 切换场景
            yield return SceneLoader.SwitchSceneSwitchCoroutine(SceneLoader.CurrentScene, sceneName);
            
            // 显示场景
            yield return panelFader.FadeCoroutine(0);
        }
    }
}
