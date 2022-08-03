using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Save
{
    [System.Serializable]
    public class SaveData
    {
        public string name;

        // 存在就是 true
        public HashSet<string> bools = new HashSet<string>();

        public Dictionary<string, string> items = new Dictionary<string, string>();

        public void Add(string key, string value)
        {
            items[key] = value;
        }

        public void AddBool(string key)
        {
            bools.Add(key);
        }
        
        public void Remove(string key)
        {
            if (items.ContainsKey(key))
                items.Remove(key);
        }

        public void RemoveBool(string key)
        {
            if (bools.Contains(key))
                bools.Remove(key);
        }

        public string GetItem(string key)
        {
            if (items.ContainsKey(key))
                return items[key];

            return "";
        }

        public bool GetBool(string key)
        {
            return bools.Contains(key);
        }

        public bool Contains(string key)
        {
            return items.ContainsKey(key);
        }
    }
}
