using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemImagePanel : BasePanel
    {
        protected Image image;

        public void SetImage(Sprite sprite, bool setNativeSize = false)
        {
            if (image == null)
                image = GetControl<Image>("ItemImage");
            image.sprite = sprite;
            
            if (setNativeSize)
                image.SetNativeSize();
        }
    }
}
