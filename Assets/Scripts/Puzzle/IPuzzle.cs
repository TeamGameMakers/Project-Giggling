namespace Puzzle
{
    /// <summary>
    /// 谜题接口，提供游戏逻辑执行接口。
    /// 通常在 Update 中执行。
    /// </summary>
    public interface IPuzzle
    {
        void Execute();
    }
}