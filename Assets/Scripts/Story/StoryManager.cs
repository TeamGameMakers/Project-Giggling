using Base;
using Base.Mono;
using Data.Story;
using UI;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Story
{
    /// <summary>
    /// EnterStroy 进入剧情。
    /// MoveNext 传递剧情进行处理。
    /// </summary>
    public class StoryManager : Singleton<StoryManager>
    {
        private PlotDataSO m_currentPlot;

        private IStoryProcessor m_processor;

        private int m_index;

        private bool m_hasChoice;

        private string panelName = "StoryPanel";

        public void StartStory(PlotDataSO plot)
        {
            // TODO: 改变游戏状态
            // 创建必要组件
            PlotProcessor pp = new PlotProcessor(panelName);
            // 显示 UI 面板
            UIManager.Instance.ShowPanel<StoryPanel>(panelName, callBack: panel => {
                // 组件注册
                pp.Register(panel.Text, panel.contentPanel);
                pp.Register(panel.LeftImage, true);
                pp.Register(panel.RightImage, false);
                PlotChoice pc = new PlotChoice(panel.choicePanel.transform);
                pp.Register(pc, panel.choicePanel);
                // 开始剧情
                EnterStory(plot, pp);
                MoveNext();
                // 开始监听输入
                MonoManager.Instance.AddUpdateListener(InputDetection);
            });
        }
        
        // 进入剧情
        public void EnterStory(PlotDataSO plot, IStoryProcessor processor)
        {
            m_currentPlot = plot;
            m_processor = processor;
            m_index = 0;
        }
        
        // 剧情前进
        public void MoveNext()
        {
            if (m_index < m_currentPlot.Count)
            {
                m_hasChoice = m_currentPlot[m_index].choices.Count > 0;
                m_processor.Process(m_currentPlot[m_index++]);
            }
            else
            {
                m_processor.End();
                EndStory();
            }
        }
        
        // 剧情结束
        public void EndStory()
        {
            // 结束输入监听
            MonoManager.Instance.RemoveUpdateListener(InputDetection);
            // TODO: 改变游戏状态
            // 关闭 UI 面板
            UIManager.Instance.HidePanel(panelName, true);
        }
        
        // 能否正常推进
        public bool CanMove()
        {
            return !m_hasChoice;
        }

        // 用户输入检测
        public void InputDetection()
        {
            if (CanMove() && InputHandler.AnyKeyPressed)
            {
                MoveNext();
            }
        }
    }
}