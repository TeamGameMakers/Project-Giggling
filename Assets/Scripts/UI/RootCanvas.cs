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
        [Tooltip("为空会在该对象上自行获取")]
        public RectTransform canvas;

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

            if (canvas == null) {
                canvas = GetComponent<RectTransform>();
            }
            
            // 找到各层
            bottom = canvas.Find(bottomName);
            middle = canvas.Find(middleName);
            top = canvas.Find(topName);
            system = canvas.Find(systemName);
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
    }
}
