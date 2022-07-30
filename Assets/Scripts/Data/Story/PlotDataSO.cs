using System.Collections.Generic;
using UnityEngine;

namespace Data.Story
{
    /// <summary>
    /// 一段剧情情节。
    /// </summary>
    [CreateAssetMenu(fileName = "PlotData", menuName = "Data/Plot Data")]
    public class PlotDataSO : ScriptableObject
    {
        public List<PlotSection> sections;

        public int Count => sections.Count;
        
        public void Add(PlotSection section)
        {
            sections.Add(section);
        }
    }
}
