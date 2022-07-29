using System;
using Data;
using UnityEngine;

namespace Interact
{
    /// <summary>
    /// 交互接口
    /// </summary>
    public abstract class Interactable: MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private InteractableDataSO _data;
        [SerializeField] private Transform _checkPoint;

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void Start()
        {
            _spriteRenderer.sprite = _data.defaultSprite;
        }

        protected virtual void Update()
        {
            if (_spriteRenderer.sprite == _data.highLightSprite)
            {
                var coll = Physics2D.
                    OverlapCircle(_checkPoint.position, _data.checkRadius, _data.checkLayer);

                if (!coll) _spriteRenderer.sprite = _data.defaultSprite;
            }
        }

        /// <summary>
        /// 可互动高亮提示
        /// </summary>
        public virtual void HighlightTip() => _spriteRenderer.sprite = _data.highLightSprite;

        /// <summary>
        /// 交互的抽象方法
        /// </summary>
        /// <param name="interactor"></param>
        public abstract void Interact(Interactor interactor);
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_checkPoint.position, _data.checkRadius);
        }
    }
}
