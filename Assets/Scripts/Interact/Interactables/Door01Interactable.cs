using UnityEngine;

namespace Interact
{
    /// <summary>
    /// 未完成。
    /// </summary>
    public class Door01Interactable : Interactable
    {
        public SpriteRenderer targetRenderer;

        protected override void Awake()
        {
            _spriteRenderer = targetRenderer;
        }

        public override void Interact(Interactor interactor)
        {
            
        }
    }
}
