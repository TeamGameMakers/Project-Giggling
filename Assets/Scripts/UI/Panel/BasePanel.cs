using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// UGUI 面板基类。
    /// 能通过代码快速的找到所有的子控件。
    /// 会自动获取 Button, Image, Text, Toggle, Slider, ScrollRect, InputField.
    /// 会绑定 Button 的 OnClick 和 Toggle 的 OnValueChanged.
    /// </summary>
    public class BasePanel : MonoBehaviour
    {
        // 通过里式转换原则 来存储所有的控件
        protected readonly Dictionary<string, List<UIBehaviour>> controlContainer = new Dictionary<string, List<UIBehaviour>>();

        protected virtual void Awake()
        {
            FindChildrenControl<Button>();
            FindChildrenControl<Image>();
            FindChildrenControl<Text>();
            FindChildrenControl<Toggle>();
            FindChildrenControl<Slider>();
            FindChildrenControl<ScrollRect>();
            FindChildrenControl<InputField>();
            FindChildrenControl<TextMeshProUGUI>();
        }

        /// <summary>
        /// 需要实现生命周期函数，以使脚本能够 disable.
        /// </summary>
        protected virtual void Start()
        {

        }

        /// <summary>
        /// 判断面板是否已经显示。
        /// </summary>
        /// <returns></returns>
        public virtual bool ShowNow()
        {
            return gameObject.activeSelf;
        }

        /// <summary>
        /// 显示自己。
        /// </summary>
        public virtual void ShowMe()
        {

        }

        /// <summary>
        /// 隐藏自己。
        /// </summary>
        public virtual void HideMe()
        {

        }

        protected virtual void OnClick(string btnName)
        {
            Debug.Log("点击按键：" + btnName);
        }

        protected virtual void OnValueChanged(string toggleName, bool value)
        {

        }

        /// <summary>
        /// 得到对应名字的对应控件脚本。
        /// </summary>
        /// <typeparam name="T">UIBehaviour 的子类</typeparam>
        /// <param name="controlName">控件名，即对象名</param>
        /// <returns></returns>
        public virtual T GetControl<T>(string controlName) where T : UIBehaviour
        {
            if (controlContainer.ContainsKey(controlName)) {
                
                for (int i = 0; i < controlContainer[controlName].Count; ++i) {
                    
                    if (controlContainer[controlName][i] is T)
                        return controlContainer[controlName][i] as T;
                }
            }

            return null;
        }

        /// <summary>
        /// 找到子对象的对应控件。
        /// 并绑定 Button 的 onClick.
        /// Toggle 的 OnValueChanged.
        /// </summary>
        /// <typeparam name="T">UIBehaviour 子类</typeparam>
        public virtual void FindChildrenControl<T>() where T : UIBehaviour
        {
            T[] coms = this.GetComponentsInChildren<T>();
            for (int i = 0; i < coms.Length; ++i) {
                
                // 将获取到的组件，按照对象区分存入
                // 去除 (Clone)
                string objName = coms[i].gameObject.name.Split('(')[0];
                if (controlContainer.ContainsKey(objName))
                    controlContainer[objName].Add(coms[i]);
                else
                    controlContainer.Add(objName, new List<UIBehaviour>() { coms[i] });

                // 如果是按钮控件
                if (coms[i] is Button)
                    (coms[i] as Button).onClick.AddListener(() => OnClick(objName)); 
                
                // 如果是单选框或者多选框
                else if (coms[i] is Toggle)
                    (coms[i] as Toggle).onValueChanged.AddListener(value => OnValueChanged(objName, value));
            }
        }
    }
}
