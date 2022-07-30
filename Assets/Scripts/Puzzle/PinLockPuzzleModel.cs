using System;
using Data;
using UnityEngine;

namespace Puzzle
{
    public class PinLockPuzzleModel : PuzzleModel
    {
        public enum State
        {
            Rising,
            Falling,
            Static
        }
        
        private PinLockDataSO m_data;
        
        // 当前推针高度
        private float m_height;
        // 推针状态
        private State m_state = State.Static;
        public State PuzzleState => m_state;
        
        private void Start()
        {
            m_data = GetData<PinLockDataSO>();
        }

        private void Update()
        {
            switch (m_state)
            {
                case State.Rising:
                    // 将推针移动到顶部
                    m_height += m_data.riseSpeed * Time.deltaTime;
                    // 抵达顶部开始下落
                    if (m_height > m_data.maxHeight)
                    {
                        m_height = m_data.maxHeight;
                        m_state = State.Falling;
                    }
                    break;
                case State.Falling:
                    // 推针下滑
                    m_height -= m_data.declineSpeed * Time.deltaTime;
                    if (m_height <= 0)
                    {
                        m_height = 0;
                        m_state = State.Static;
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
            if (Mathf.Abs(m_height - m_data.unlockHeight) <= m_data.unlockRange)
            {
                m_data.solved = true;
                return true;
            }

            return false;
        }
    }
}
