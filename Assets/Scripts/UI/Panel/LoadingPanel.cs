using System.Collections;
using Base.Scene;
using Data;
using TMPro;

namespace UI
{
    public class LoadingPanel : BasePanel
    {
        protected bool ready = false;
        protected bool canClose = false;
        protected bool canFade = false;

        public TextMeshProUGUI content;
        public TextMeshProUGUI hint;
        
        protected CanvasGroupFader panelFader;
        protected CanvasGroupFader contentFader;

        public bool waitInput = false;

        public LoadingPanelContent loadingContent;

        protected override void Start()
        {
            base.Start();

            panelFader = GetComponent<CanvasGroupFader>();
            contentFader = transform.Find("ContainerPanel").GetComponent<CanvasGroupFader>();
            // 设置随机文字
            content.SetText(loadingContent.GetRandom());

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
            
            // // 加载场景
            // yield return SceneLoader.SwitchSceneLoadCoroutine(sceneName);

            // 加载场景
            yield return SceneLoader.LoadSceneAsyncCoroutine(sceneName, null);

            hint.gameObject.SetActive(true);
            if (waitInput)
            {
                // 等待切换
                canClose = true;
                while (!canFade)
                {
                    yield return null;
                }
                yield return contentFader.FadeCoroutine(0);
            }
            
            // // 切换场景
            // yield return SceneLoader.SwitchSceneSwitchCoroutine(SceneLoader.CurrentScene, sceneName);
            
            // 显示场景
            yield return panelFader.FadeCoroutine(0);
            
            // 关闭自己
            UIManager.Instance.HidePanel("LoadingPanel");
        }
    }
}
