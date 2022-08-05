using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemImagePanel : BasePanel
    {
        protected Image image;
        
        public void SetImage(Sprite sprite, Vector3 scale, bool setNativeSize = false)
        {
            if (image == null)
                image = GetControl<Image>("ItemImage");
            image.sprite = sprite;
            
            if (setNativeSize)
                image.SetNativeSize();

            image.transform.localScale = scale;
        }
    }
}
