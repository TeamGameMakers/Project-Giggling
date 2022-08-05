using UI;
using UnityEngine;

namespace Interact
{
    public class ImageInteractable : Interactable
    {
        public Sprite sprite;

        public bool setNativeSize = true;
        
        public Vector3 scale = Vector3.one;
        
        public override void Interact(Interactor interactor)
        {
            UIManager.Instance.ShowPanel<ItemImagePanel>("ItemImagePanel", callBack: panel => panel.SetImage(sprite, scale, setNativeSize));
        }
    }
}
