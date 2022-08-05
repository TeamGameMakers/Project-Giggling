using System;
using System.Collections.Generic;
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
        
        private static Stack<GameState> stateStake = new Stack<GameState>();

        public static void ClearStateRecord()
        {
            stateStake.Clear();
        }
        
        /// <summary>
        /// 返回上一个状态。
        /// </summary>
        /// <param name="def">没有记录时设置的默认状态</param>
        public static void BackGameState(GameState def = GameState.Playing)
        {
            if (stateStake.Count == 0)
            {
                Debug.Log("无状态可恢复");
                SwitchGameState(def, false);
            }
            else
                SwitchGameState(stateStake.Pop(), false);
        }
        
        public static void SwitchGameState(GameState state, bool pushInStack = true)
        {
            Debug.Log("进入: " + state);
            
            if (pushInStack)
                stateStake.Push(m_state);
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
                case GameState.CG:
                    InputHandler.SwitchToUI();
                    break;
            }

            SwitchStateEvent?.Invoke(m_state);
        }

        public static void SetPlayerTransform(Transform player) => Player = player;
    }
}
