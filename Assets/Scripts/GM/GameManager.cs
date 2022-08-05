using System;
using UnityEngine;

namespace GM
{
    public enum GameState
    {
        // 游玩
        Playing,
        // 剧情
        Story,
        // 解谜
        Puzzle,
        // 推针
        PinLock,
        CG,
        UI
    }
    
    public static class GameManager
    {
        private static GameState m_state = GameState.Playing;
        
        public static GameState State => m_state;

        public static event Action<GameState> SwitchStateEvent;

        public static Transform Player { get; private set; }

        // 最好手动设置
        public static GameState lastState = GameState.Playing;
        // 该方法自动记录，容易出错
        // 该方法应该由一个栈进行维护
        public static void BackGameState()
        {
            SwitchGameState(lastState);
        }
        
        public static void SwitchGameState(GameState state)
        {
            Debug.Log("进入: " + state);
            lastState = m_state;
            m_state = state;
            // 切换 map
            switch (m_state)
            {
                case GameState.Playing:
                    InputHandler.SwitchToPlayer();
                    break;
                case GameState.Story:
                    // 切成 UI map, 无输入，直接读取任意键
                    InputHandler.SwitchToUI();
                    break;
                case GameState.PinLock:
                    InputHandler.SwitchToLockPick();
                    break;
                case GameState.UI:
                    InputHandler.SwitchToUI();
                    break;
            }

            SwitchStateEvent?.Invoke(m_state);
        }

        public static void SetPlayerTransform(Transform player) => Player = player;
    }
}
