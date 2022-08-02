using Data;
using UI;
using UnityEngine;

namespace Interact
{
    public class ItemInteractable : Interactable
    {
        public ItemDataSO item;
        
        public override void Interact(Interactor interactor)
        {
            UIManager.Instance.ShowPanel<ItemInfoPanel>("ItemInfoPanel", callBack: panel => panel.UpdateInfo(item));
        }
    }
}
