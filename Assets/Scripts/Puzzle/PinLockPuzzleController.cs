using Base.Event;
using GM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Puzzle
{
    /// <summary>
    /// 接受用户输入，并执行 Model.
    /// </summary>
    public class PinLockPuzzleController : PuzzleController
    {
        private PinLockPuzzleModel m_model;
        
        protected string successEvent = "PinLockSuccessEvent";
        protected string failEvent = "PinLockFailEvent";
        
        private void Start()
        {
            m_model = GetModel<PinLockPuzzleModel>();
            GameManager.SwitchGameState(GameState.PinLock);
        }

        private void Update()
        {
            // 点击任意区域退出
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                GameManager.BackGameState();
                Destroy(gameObject);
                return;
            }
            
            switch (m_model.PuzzleState)
            {
                case PinLockPuzzleModel.State.Rising:
                    // 上升时没有操作
                    break;
                case PinLockPuzzleModel.State.Falling:
                    // 下落时检查解锁
                    if (InputHandler.PickPressed)
                    {
                        GameManager.BackGameState();
                        if (m_model.TryUnlock())
                        {
                            Debug.Log("解谜成功");
                            EventCenter.Instance.FuncTrigger(successEvent);
                        }
                        else
                        {
                            Debug.Log("解谜失败");
                            EventCenter.Instance.FuncTrigger(failEvent);
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
