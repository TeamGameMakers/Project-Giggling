using Data;
using UnityEngine;

namespace Puzzle
{
    /// <summary>
    /// 谜题模型。
    /// 提供谜题数据，及谜题本身的运行逻辑。
    /// 提供操作谜题的方法，但不处理用户操作。
    /// </summary>
    public abstract class PuzzleModel : PuzzleBase
    {
        // public PuzzleDataSO dataPrefab;
        //
        // protected T GetData<T>() where T : PuzzleDataSO
        // {
        //     return ScriptableObject.Instantiate(dataPrefab) as T;
        // }
    }
}
