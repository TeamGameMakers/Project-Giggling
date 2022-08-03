using System;
using System.Collections.Generic;
using Base.Resource;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    /// <summary>
    /// 自动生成剧情选择按键，并绑定点击事件。
    /// </summary>
    public class PlotChoice : IChoiceList
    {
        protected Transform parent;
        protected List<Button> buttons = new List<Button>();
        protected Action<int> choose;

        public PlotChoice(Transform parent)
        {
            this.parent = parent;
        }
        
        public void ShowChoices(List<string> choices)
        {
            for (int i = 0; i < choices.Count; ++i)
            {
                GameObject go = ResourceLoader.Load<GameObject>("Prefabs/UI/Component/PlotBtn");
                go.name += i;
                go.transform.SetParent(parent);
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

        public void HideChoices()
        {
            for (int i = buttons.Count - 1; i >= 0; --i)
            {
                GameObject.Destroy(buttons[i]);
            }
            buttons.Clear();
        }

        public void RegisterChoose(Action<int> chooseActions)
        {
            choose = chooseActions;
        }
    }
}