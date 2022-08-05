using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemImagePanel : BasePanel
    {
        protected Image image;

        public void SetImage(Sprite sprite)
        {
            if (image == null)
                image = GetControl<Image>("ItemImage");
            image.sprite = sprite;
        }
    }
}
