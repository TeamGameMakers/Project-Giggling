using Base;
using Data.Story;
using UnityEngine;

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
            
        }
        
        // 能否正常推进
        public bool CanMove()
        {
            return !m_hasChoice;
        } 
    }
}
