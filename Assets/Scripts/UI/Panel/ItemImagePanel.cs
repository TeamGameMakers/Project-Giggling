using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemImagePanel : BasePanel
    {
        protected Button cgBtn;
        public Button CgBtn => cgBtn;
        
        protected Image image;

        protected override void Awake()
        {
            base.Awake();
            cgBtn = transform.Find("CloseBgBtn").GetComponent<Button>();
        }

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
