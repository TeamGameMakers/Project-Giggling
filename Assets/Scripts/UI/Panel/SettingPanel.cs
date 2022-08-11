using System.Collections.Generic;
using Base.Scene;
using GM;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class SettingPanel : BasePanel
    {
        // 当前面板索引
        protected int index = 0;
        // 面板列表
        [SerializeField]
        protected List<GameObject> panelObjs = new List<GameObject>();

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "KeyBtn":
                    AkSoundEngine.PostEvent("Menu_exit", gameObject);
                    SwitchPanel(0);
                    break;
                case "SoundBtn":
                    AkSoundEngine.PostEvent("Menu_exit", gameObject);
                    SwitchPanel(1);
                    break;
                case "CreditsBtn":
                    AkSoundEngine.PostEvent("Menu_exit", gameObject);
                    SwitchPanel(2);
                    break;
                case "QuitBtn":
                    AkSoundEngine.PostEvent("Menu_exit", gameObject);
                    if (SceneLoader.CurrentScene == "00_Phase_0")
                    {
                        UIManager.Instance.HidePanel("SettingPanel", true);
                        return;
                    }
                    UIManager.Instance.HidePanel("StartPanel", true);
                    UIManager.Instance.HidePanel("GamePanel", true);
                    UIManager.Instance.HidePanel("Status Panel", true);
                    UIManager.Instance.HidePanel("SettingPanel", true);
                    SceneLoader.LoadScene("00_Phase_0");
                    break;
            }
        }

        protected void SwitchPanel(int i)
        {
            if (index == i)
                return;
            
            panelObjs[index].gameObject.SetActive(false);
            index = i;
            panelObjs[index].gameObject.SetActive(true);
        }
    }
}
