using Data.Story;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
    public class ItemDataSO : ScriptableObject
    {
        public new string name;
        [TextArea]
        public string info;
        public Sprite icon;
        public bool canPick = false;
        [Tooltip("第一次拾取对话")]
        public PlotDataSO firstPlot;

        public string FirstSaveKey {
            get {
                if (firstPlot != null)
                    return "first_pick_" + firstPlot.name;
                return "";
            }
        }
    }
}
