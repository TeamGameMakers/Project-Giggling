using UI;
using UnityEngine;

namespace Interact
{
    public class ImageInteractable : Interactable
    {
        public Sprite sprite;
        
        public override void Interact(Interactor interactor)
        {
            UIManager.Instance.ShowPanel<ItemImagePanel>("ItemImagePanel", callBack: panel => panel.SetImage(sprite));
        }
    }
}
