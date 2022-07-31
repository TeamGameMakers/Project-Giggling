using System.Collections.Generic;
using System.Linq;

namespace Save
{
    [System.Serializable]
    public class SaveData
    {
        public string name;

        public Dictionary<string, string> items = new Dictionary<string, string>();

        public void Add(string key, string value)
        {
            items[key] = value;
        }
        
        public void Remove(string key)
        {
            if (items.ContainsKey(key))
            {
                items.Remove(key);
            }
        }

        public string GetItem(string key)
        {
            if (items.ContainsKey(key))
            {
                return items[key];
            }

            return "";
        }
    }
}
