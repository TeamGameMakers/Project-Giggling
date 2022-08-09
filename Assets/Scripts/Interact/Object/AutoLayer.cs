using System;
using UnityEngine;

namespace Interact
{
    public class AutoLayer : MonoBehaviour
    {
        [Tooltip("同物体上能自动获取")]
        public new SpriteRenderer renderer;
        
        public LayerMask layerMask;
        public string defaultLayer = "CoverByPlayer";
        public string changeLayer = "CoverPlayer";

        private void Awake()
        {
            if (renderer == null)
                renderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            renderer.sortingLayerName = changeLayer;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            renderer.sortingLayerName = defaultLayer;
        }
    }
}
