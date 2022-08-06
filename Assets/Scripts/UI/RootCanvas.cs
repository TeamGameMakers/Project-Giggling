using Base;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// UI 层级。
    /// </summary>
    public enum UILayer
    {
        Bottom,
        Middle,
        Top,
        System
    }

    /// <summary>
    /// 根画布，提供 UI 层级。
    /// </summary>
    public class RootCanvas : SingletonMono<RootCanvas>
    {
        protected RectTransform canvas;
        public RectTransform Canvas {
            get {
                if (canvas == null)
                    canvas = GetComponent<RectTransform>();
                return canvas;
            }
        }

        // Bottom 层级物体名
        public string bottomName = "Bottom";
        // Middle 层级物体名
        public string middleName = "Middle";
        // Top 层级物体名
        public string topName = "Top";
        // System 层级物体名
        public string systemName = "System";
        
        protected Transform bottom;
        protected Transform middle;
        protected Transform top;
        protected Transform system;

        protected override void Awake()
        {
            base.Awake();

            // 找到各层
            bottom = Canvas.Find(bottomName);
            middle = Canvas.Find(middleName);
            top = Canvas.Find(topName);
            system = Canvas.Find(systemName);
        }

        /// <summary>
        /// 通过层级枚举，得到对应层级的父对象。
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Transform GetLayerRoot(UILayer layer)
        {
            switch (layer) {
                case UILayer.Bottom:
                    return bottom;
                case UILayer.Middle:
                    return middle;
                case UILayer.Top:
                    return top;
                case UILayer.System:
                    return system;
            }
            return null;
        }

        public void ShowAll()
        {
            bottom.gameObject.SetActive(true);
            middle.gameObject.SetActive(true);
            top.gameObject.SetActive(true);
            system.gameObject.SetActive(true);
        }

        public void HideAll()
        {
            bottom.gameObject.SetActive(false);
            middle.gameObject.SetActive(false);
            top.gameObject.SetActive(false);
            system.gameObject.SetActive(false);
        }
    }
}
