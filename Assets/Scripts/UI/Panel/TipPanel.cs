using TMPro;
using UnityEngine;

namespace UI
{
    public class TipPanel : BasePanel
    {
        protected TextMeshProUGUI tmp;

        public TextMeshProUGUI Tmp {
            get {
                if (tmp == null) {
                    tmp = GetControl<TextMeshProUGUI>("Content");
                }

                return tmp;
            }
        }

        public void SetContent(string content)
        {
            Tmp.SetText(content);
        }

        public override void HideMe()
        {
            base.HideMe();
            gameObject.SetActive(false);
        }
    }
}
