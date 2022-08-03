using GM;
using Save;
using UnityEngine;

namespace Puzzle
{
    /// <summary>
    /// 接受用户输入，并执行 Model.
    /// </summary>
    public class PinLockPuzzleController : PuzzleController
    {
        public string saveKey = "PinLock";
        
        private PinLockPuzzleModel m_model;
        
        private void Start()
        {
            m_model = GetModel<PinLockPuzzleModel>();
            GameManager.SwitchGameState(GameState.PinLock);
        }

        private void Update()
        {
            switch (m_model.PuzzleState)
            {
                case PinLockPuzzleModel.State.Rising:
                    // 上升时没有操作
                    break;
                case PinLockPuzzleModel.State.Falling:
                    // 下落时检查解锁
                    if (InputHandler.PickPressed)
                    {
                        if (m_model.TryUnlock())
                        {
                            Debug.Log("解谜成功");
                            // 解锁成功的逻辑
                            GameManager.SwitchGameState(GameState.Playing);
                            Destroy(gameObject);
                            // 存档
                            SaveManager.RegisterBool(saveKey);
                            // TODO: 成功事件
                        }
                        else
                        {
                            Debug.Log("解谜失败");
                            // TODO: 解锁失败的逻辑
                        }
                    }
                    break;
                case PinLockPuzzleModel.State.Static:
                    // 静止时检查推针
                    if (InputHandler.PryInput)
                    {
                        m_model.PushPin();
                    }
                    break;
            }
        }
    }
}
