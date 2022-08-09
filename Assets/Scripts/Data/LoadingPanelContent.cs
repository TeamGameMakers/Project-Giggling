using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LoadingContent", menuName = "Data/Loading Content")]
    public class LoadingPanelContent : ScriptableObject
    {
        public List<string> contents;

        public string GetRandom()
        {
            return contents[Random.Range(0, contents.Count)];
        }
    }
}