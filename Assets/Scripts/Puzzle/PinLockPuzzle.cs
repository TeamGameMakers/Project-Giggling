using System;
using Data;
using UnityEngine;

namespace Puzzle
{
    public class PinLockPuzzle : IPuzzle
    {
        // 当前推针高度
        public float height;
        
        private readonly PinLockDataSO m_data;
        private State m_state = State.Static;
        private readonly Action m_start;
        private readonly Action m_hint;
        private readonly Action m_unlock;
        private readonly Action m_fail;
        private readonly Action m_exit;

        private bool m_hintOnce = false;
        
        /// <param name="data"></param>
        /// <param name="hint">可以解锁时的提示行为</param>
        /// <param name="unlock">解锁行为</param>
        public PinLockPuzzle(PinLockDataSO data, 
            Action start, Action hint, Action unlock, Action fail, Action exit)
        {
            m_data = data;

            m_start = start;
            m_hint = hint;
            m_unlock = unlock;
            m_fail = fail;
            m_exit = exit;
        }
        
        public void Execute()
        {
            // 退出谜题输入
            // TODO: 接受退出输入
            m_exit?.Invoke();
            
            switch (m_state) {
                case State.Rising:
                    // 更新高度，不接受输入
                    height += m_data.riseSpeed * Time.deltaTime;
                    if (height > m_data.maxHeight) {
                        height = m_data.maxHeight;
                        m_state = State.Falling;
                    }
                    break;
                case State.Falling:
                    // 更新高度，并在恰当的时机提示解锁
                    height -= m_data.declineSpeed * Time.deltaTime;
                    if (!m_hintOnce && height <= m_data.unlockHeight) {
                        m_hint?.Invoke();
                        m_hintOnce = true;
                    }
                    // 接受输入，并进行不同处理
                    // TODO: 接受解锁输入
                    if (true) {
                        if (height <= m_data.unlockHeight &&
                            height >= m_data.unlockHeight - m_data.unlockRange) {
                            m_unlock?.Invoke();
                        }
                        else {
                            // 解锁失败
                            m_fail?.Invoke();
                        }
                    }
                    // 结束状态检测
                    if (height <= 0) {
                        height = 0;
                        m_state = State.Static;
                        m_hintOnce = false;
                    }
                    break;
                case State.Static:
                    // 接受用户开始推针输入
                    // TODO: 接受开始推针输入
                    m_start?.Invoke();
                    m_state = State.Rising;
                    break;
            }
        }

        private enum State
        {
            Rising,
            Falling,
            Static
        }
    }
}
