using System;
using System.Collections.Generic;
using Base;
using Base.Mono;
using Base.Resource;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        public string resourceDir = "Prefabs/UI";
        
        public readonly Dictionary<string, BasePanel> panelContainer = new Dictionary<string, BasePanel>();

        #region 创建实际面板

        public void CreateFade(float duration = 0.5f, float originalAlpha = 1, float targetAlpha = 0)
        {
            Instance.ShowPanel<FaderPanel>("Fader", "FaderPanel", UILayer.Top, panel => {
                panel.fader.Alpha = originalAlpha;
                panel.fader.fadeDuration = duration;
                panel.fader.Fade(targetAlpha);
            });
        }

        public void ShowHint(string content)
        {
            Instance.ShowPanel<TipPanel>("HintPanel", "HintPanel", UILayer.Top, panel => {
                panel.SetContent(content);
            });
        }

        public void HideHint()
        {
            Instance.HidePanel("HintPanel");
        }

        public void ShowGameOverPanel()
        {
            Instance.ShowPanel<GameOverPanel>("GameOverPanel", "", UILayer.Top);
        }

        #endregion

        /// <summary>
        /// 显示面板。
        /// </summary>
        /// <typeparam name="T">面板脚本类型</typeparam>
        /// <param name="name">记录的键值</param>
        /// <param name="subPath">resourceDir 目录下的相对路径，包括预制体名，如 "Menu/SettingMenu". 为空则直接使用 name</param>
        /// <param name="layer">显示在哪一层</param>
        /// <param name="callBack">面板创建成功后的回调</param>
        public void ShowPanel<T>(string name, string subPath = "", UILayer layer = UILayer.Middle, Action<T> callBack = null) where T : BasePanel
        {
            // 该面板已经存在
            if (panelContainer.ContainsKey(name))
            {
                T panel = panelContainer[name] as T;
                // 已经显示了
                if (panel.ShowNow())
                    return;
                
                Transform father = RootCanvas.Instance.GetLayerRoot(layer);
                panel.transform.SetParent(father);
                panel.ShowMe();
                // 面板创建完成后回调
                callBack?.Invoke(panel);
                // 直接结束
                return;
            }

            if (string.IsNullOrEmpty(subPath))
            {
                subPath = name;
            }
            // 面板不存在，则加载预制体
            ResourceLoader.LoadAsync<GameObject>( resourceDir + "/" + subPath, (obj) => {
                if (obj == null)
                {
                    Debug.LogWarning($"资源'{subPath}`加载失败");
                    return;
                }
                
                obj.name = name;
                Transform father = RootCanvas.Instance.GetLayerRoot(layer);

                // 设置父对象
                obj.transform.SetParent(father);
                
                // 将相对位置置零
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                (obj.transform as RectTransform).offsetMax = Vector2.zero;
                (obj.transform as RectTransform).offsetMin = Vector2.zero;

                T panel = obj.GetComponent<T>();
                // 面板创建完成后回调
                callBack?.Invoke(panel);

                panel.ShowMe();

                // 因为异步原因，会出现重复存在的情况
                if (panelContainer.ContainsKey(name))
                {
                    Debug.LogWarning("面板已存在");
                    HidePanel(name, true);
                }
                // 把面板存起来
                panelContainer.Add(name, panel);
            });
        }

        /// <summary>
        /// 隐藏面板。
        /// </summary>
        /// <param name="name">面板名</param>
        /// <param name="destroy">是否同时销毁面板</param>
        public void HidePanel(string name, bool destroy = false)
        {
            if (panelContainer.ContainsKey(name)) 
            {
                panelContainer[name].HideMe();
                if (destroy) 
                {
                    GameObject.Destroy(panelContainer[name].gameObject);
                    panelContainer.Remove(name);
                }
            }
        }

        /// <summary>
        /// 得到某一个已经显示的面板，方便外部使用。
        /// </summary>
        /// <param name="name">面板名</param>
        public T GetPanel<T>(string name) where T : BasePanel
        {
            if (panelContainer.ContainsKey(name))
                return panelContainer[name] as T;
            return null;
        }

        /// <summary>
        /// 给 UI 控件添加自定义事件。
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="type">事件类型</param>
        /// <param name="callback">回调</param>
        public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callback)
        {
            EventTrigger trigger = control.GetComponent<EventTrigger>();
            if (trigger == null) {
                trigger = control.gameObject.AddComponent<EventTrigger>();
            }

            // 添加事件
            EventTrigger.Entry entry = new EventTrigger.Entry {
                eventID = type
            };
            entry.callback.AddListener(callback);
            trigger.triggers.Add(entry);
        }
    }
}
