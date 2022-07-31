using System;
using System.Collections.Generic;
using Base.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ChoicePanel : BasePanel, IChoiceList
    {
        protected List<Button> buttons = new List<Button>();
        protected Action<int> choose;

        public void ShowChoices(List<string> choices)
        {
            // 创建按钮并绑定事件
            for (int i = 0; i < choices.Count; ++i)
            {
                GameObject go = ResourceLoader.Load<GameObject>("Prefabs/UI/Component/PlotBtn");
                go.name += i;
                go.transform.SetParent(transform);
                Button btn = go.GetComponent<Button>();
                TextMeshProUGUI tmp = btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                tmp.SetText(choices[i]);

                int t = i;
                btn.onClick.AddListener(() => {
                    Debug.Log("点击了剧情按键:" + t);
                    choose(t);
                });
                buttons.Add(btn);
            }
        }

        public void HideSelf()
        {
            for (int i = buttons.Count - 1; i >= 0; --i)
            {
                Destroy(buttons[i]);
            }
            buttons.Clear();
            gameObject.SetActive(false);
        }

        public void RegisterChoose(Action<int> chooseActions)
        {
            choose = chooseActions;
        }
    }
}
