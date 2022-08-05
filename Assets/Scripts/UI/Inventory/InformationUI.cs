using TMPro;
using UnityEngine;

namespace UI.Inventory
{
    public class InformationUI : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetInformation(string info)
        {
            _textMeshPro.text = info;
        }
    }
}
