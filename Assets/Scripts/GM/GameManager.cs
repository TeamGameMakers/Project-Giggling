using System;
using UnityEngine;

namespace GM
{
    public enum GameState
    {
        // 游玩
        Playing,
        // 剧情
        Story
    }
    
    public static class GameManager
    {
        private static GameState m_state = GameState.Playing;

        public static GameState State => m_state;

        public static event Action<GameState> SwitchStateEvent;

        public static void SwitchGameState(GameState state)
        {
            m_state = state;
            // TODO: 切换 map

            SwitchStateEvent?.Invoke(m_state);
        }
    }
}
