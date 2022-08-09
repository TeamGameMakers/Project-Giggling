using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class TipConfirmPanel : TipPanel
    {
        protected Button confirmBtn;

        public Button ConfirmBtn {
            get {
                if (confirmBtn == null)
                    confirmBtn = GetControl<Button>("Confirm");
                return confirmBtn;
            }
        }

        public void AddButtonEvent(UnityAction callback)
        {
            AkSoundEngine.PostEvent("Menu_confirm", gameObject);
            ConfirmBtn.onClick.AddListener(callback);
        }
    }
}
