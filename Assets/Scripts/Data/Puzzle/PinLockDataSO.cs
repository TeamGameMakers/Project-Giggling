using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PinLockData", menuName = "Data/Pin Lock Data")]
    public class PinLockDataSO : PuzzleDataSO
    {
        // 推针最大高度
        public float maxHeight;
        // 推针解锁高度
        public float unlockHeight;
        // 解锁范围
        public float unlockRange;
        // 上升速度
        public float riseSpeed;
        // 下滑速度
        public float declineSpeed;
    }
}
