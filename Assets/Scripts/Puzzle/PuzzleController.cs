using UnityEngine;

namespace Puzzle
{
    /// <summary>
    /// 该类负责处理玩家对谜题的操作。
    /// </summary>
    public abstract class PuzzleController : MonoBehaviour
    {
        protected T GetModel<T>(GameObject obj = null) where T : PuzzleModel
        {
            if (obj == null)
            {
                return GetComponent<T>();
            }

            return obj.GetComponent<T>();
        }
    }
}
