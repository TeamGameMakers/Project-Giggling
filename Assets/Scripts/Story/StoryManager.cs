using Base;
using Data.Story;

namespace Story
{
    public class StoryManager : Singleton<StoryManager>
    {
        private PlotDataSO m_currentPlot;

        private int m_index;
        
        // 进入剧情
        public void EnterStory(PlotDataSO plot)
        {
            m_currentPlot = plot;
            m_index = 0;
        }
        
        // 剧情前进，应该由一个剧情处理器处理
        public void MoveNext()
        {
            if (m_index >= m_currentPlot.Count)
            {
                
            }
        }
        
        // 剧情结束
    }
}
