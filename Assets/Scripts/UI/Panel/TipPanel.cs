using GM;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TipPanel : BasePanel
    {
        public GameState switchState = GameState.Playing;
        
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

        public override void ShowMe()
        {
            base.ShowMe();
            GameManager.BackGameState();
            gameObject.SetActive(true);
        }

        public override void HideMe()
        {
            base.HideMe();
            gameObject.SetActive(false);
            GameManager.BackGameState();
        }
    }
}
