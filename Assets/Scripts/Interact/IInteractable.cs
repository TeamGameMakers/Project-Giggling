namespace Interact
{
    /// <summary>
    /// 交互接口
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// 操作提示
        /// </summary>
        public string InteractPrompt { get; }
        
        /// <summary>
        /// 交互的抽象方法
        /// </summary>
        /// <param name="interactor"></param>
        public void Interact(Interactor interactor);
    }
}
