namespace Puzzle
{
    public class PinLockPuzzleController : PuzzleController
    {
        private PinLockPuzzleModel m_model;
        
        private void Start()
        {
            m_model = GetModel<PinLockPuzzleModel>();
            InputHandler.SwitchToLockPick();
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
                    if (InputHandler.PickPressed && m_model.TryUnlock())
                    {
                        // 解锁成功的逻辑
                    }
                    else
                    {
                        // 解锁失败的逻辑
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
