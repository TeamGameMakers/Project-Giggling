using Base.Scene;
using Save;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartPanel : BasePanel
    {
        protected bool existsSave;
        
        protected override void Start()
        {
            base.Start();
            // 设置“继续游戏”是否存在
            existsSave = SaveManager.ExistsSave(SaveManager.saveName);
            if (!existsSave)
            {
                GetControl<Button>("ContinueBtn").gameObject.SetActive(false);
            }
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            
            switch (btnName)
            {
                case "ContinueBtn":
                    // 加载存档进入游戏
                    SaveManager.Load();
                    // 根据存档加载场景
                    string json = SaveManager.GetValue("SceneName");
                    if (!string.IsNullOrEmpty(json))
                        EnterLoadingPanel(json);
                    break;
                case "NewGameBtn":
                    // 检查存档并做出提示
                    if (existsSave)
                    {
                        // 提示会覆盖存档
                        UIManager.Instance.ShowPanel<TipConfirmPanel>("TipConfirmPanel", callBack: panel => {
                            panel.SetContent("新游戏将会覆盖存档!");
                            panel.AddButtonEvent(() => {
                                // 删除存档
                                SaveManager.Delete(SaveManager.saveName);
                                // 关闭自身
                                UIManager.Instance.HidePanel("TipConfirmPanel");
                                // 加载场景
                                EnterLoadingPanel("01_Phase_1");
                            });
                        });
                    }
                    else
                    {
                        // 加载场景
                        EnterLoadingPanel("01_Phase_1");
                    }
                    break;
                case "SettingBtn":
                    // 显示设置面板
                    UIManager.Instance.ShowPanel<SettingPanel>("SettingPanel");
                    break;
                case "QuitBtn":
#if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                    break;
            }
        }

        protected void EnterLoadingPanel(string scene)
        {
            UIManager.Instance.ShowPanel<LoadingPanel>("LoadingPanel" , "", UILayer.System, panel => {
                panel.LoadScene(scene);
            });
            UIManager.Instance.HidePanel("StartPanel", true);
        }
    }
}
