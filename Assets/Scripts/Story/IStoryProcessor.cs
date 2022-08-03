using Data.Story;

namespace Story
{
    /// <summary>
    /// 剧情处理器，接受一节剧情进行处理。
    /// </summary>
    public interface IStoryProcessor
    {
        void Process(PlotSection section);

        void End();
    }
}