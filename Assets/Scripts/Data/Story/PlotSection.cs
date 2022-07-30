using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Data.Story
{
    /// <summary>
    /// 一段情节中的每节内容。
    /// </summary>
    [System.Serializable]
    public class PlotSection
    {
        // 剧情文本
        public string text;
        // 剧情立绘
        public Sprite sprite;
        // 特殊事件
        public UnityAction action;
        // 剧情选择
        public List<PlotDataSO> choices;
    }
}
