using System;
using GM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 第一个子对象，铺满全屏，
    /// 提供点击“空白位置”关闭面板的功能。
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class PanelCloseButton : MonoBehaviour
    {
        [Tooltip("会自动设置为父对象")]
        public BasePanel closePanel;

        public bool destroyPanel = true;

        protected Button button;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
        }

        protected virtual void Start()
        {
            if (closePanel == null)
            {
                closePanel = transform.parent.GetComponent<BasePanel>();
            }
            
            button.onClick.AddListener(() => {
                UIManager.Instance.HidePanel(closePanel.gameObject.name, destroyPanel);
            });
        }

        public void AddButtonListener(UnityAction action)
        {
            button.onClick.AddListener(action);
        }

        public void RemoveButtonListener(UnityAction action)
        {
            button.onClick.RemoveListener(action);
        }
    }
}