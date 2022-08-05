using System.Collections.Generic;
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

        protected override void Start()
        {
            base.Start();
            GameManager.SwitchGameState(GameState.UI);
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "KeyBtn":
                    SwitchPanel(0);
                    break;
                case "SoundBtn":
                    SwitchPanel(1);
                    break;
                case "CreditsBtn":
                    SwitchPanel(2);
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

        public override void HideMe()
        {
            base.HideMe();
            GameManager.SwitchGameState(GameState.Playing);
        }
    }
}
