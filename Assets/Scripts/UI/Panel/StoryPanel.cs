using System;
using Data.Story;
using Story;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 创建 IStoryProcessor,
    /// 绑定 IChoiceList,
    /// 接受输入，使用 StoryManager 推动剧情。
    /// </summary>
    public class StoryPanel : BasePanel
    {
        public GameObject contentPanel;
        public GameObject choicePanel;

        protected PlotController pc;
        
        protected override void Awake()
        {
            base.Awake();
            // 创建剧情处理器
            pc = new PlotController("StoryPanel");
            pc.Register(GetControl<Image>("LeftImage"), true);
            pc.Register(GetControl<Image>("RightImage"), false);
            pc.Register(GetControl<TextMeshProUGUI>("PlotContent"), contentPanel);
            pc.Register(choicePanel.GetComponent<ChoicePanel>(), choicePanel);
        }

        protected virtual void Update()
        {
            // 接受输入推进剧情
            if (InputHandler.AnyKeyPressed && StoryManager.Instance.CanMove())
            {
                Debug.Log("剧情前进");
                StoryManager.Instance.MoveNext();
            }
        }

        // 开始剧情
        public void EnterStory(PlotDataSO plot)
        {
            StoryManager.Instance.EnterStory(plot, pc);
            StoryManager.Instance.MoveNext();
        }
    }
}
