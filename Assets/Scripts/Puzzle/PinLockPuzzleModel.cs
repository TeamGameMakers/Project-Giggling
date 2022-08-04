using UnityEngine;

namespace Puzzle
{
    /// <summary>
    /// 处理谜题逻辑，并提供出去。
    /// </summary>
    public class PinLockPuzzleModel : PuzzleModel
    {
        public enum State
        {
            Rising,
            Falling,
            Static
        }

        [Header("推针参数")] 
        [HideInInspector]
        public float maxHeight;
        [Tooltip("上升速度")]
        public float riseSpeed;
        [Tooltip("下滑速度")]
        public float declineSpeed;
        [Tooltip("推针上升速度")]
        public float pinSpeed;
        [Tooltip("推针最大高度")]
        public float pinHeight;
        [Tooltip("解锁触发器")]
        public PinLockUnlockZone zone;
        
        // 当前高度
        private float m_height;
        public float Height => m_height;
        
        // 推针高度
        private float m_pinH;
        public float PinH => m_pinH;
        
        // 推针状态
        private State m_state = State.Static;
        public State PuzzleState => m_state;

        private void Update()
        {
            switch (m_state)
            {
                case State.Rising:
                    // 移动推针
                    m_pinH += pinSpeed * Time.deltaTime;
                    if (m_pinH > pinHeight)
                    {
                        m_pinH = pinHeight;
                    }
                    
                    // 移动到顶部
                    m_height += riseSpeed * Time.deltaTime;
                    // 抵达顶部开始下落
                    if (m_height > maxHeight)
                    {
                        m_height = maxHeight;
                        m_state = State.Falling;
                    }
                    break;
                case State.Falling:
                    // 下滑
                    m_height -= declineSpeed * Time.deltaTime;
                    if (m_height <= 0)
                    {
                        m_height = 0;
                        m_state = State.Static;
                    }
                    // 撞到推针
                    if (m_height <= m_pinH)
                    {
                        m_pinH = m_height;
                    }
                    break;
                case State.Static:
                    // 静止下没有动作
                    break;
            }
        }

        // 推针，即进入 Rising 状态
        public void PushPin()
        {
            m_state = State.Rising;
        }

        // 尝试解锁
        public bool TryUnlock()
        {
            if (zone.Inside)
            {
                return true;
            }

            return false;
        }
    }
}
