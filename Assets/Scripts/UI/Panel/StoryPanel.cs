using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 提供 UI 控件，交由 StoryManager 管理。
    /// </summary>
    public class StoryPanel : BasePanel
    {
        public GameObject contentPanel;
        public GameObject choicePanel;

        public Image LeftImage => GetControl<Image>("LeftImage");
        public Image RightImage => GetControl<Image>("RightImage");
        public TextMeshProUGUI Text => GetControl<TextMeshProUGUI>("PlotContent");
    }
}
