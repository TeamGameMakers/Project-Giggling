using System.Collections.Generic;

namespace Save
{
    [System.Serializable]
    public class SaveItem
    {
        public string key;
        public string value;

        public SaveItem()
        {
        }

        public SaveItem(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public override string ToString()
        {
            return $"[{key}:{value}]" + base.ToString();
        }
    }

    [System.Serializable]
    public class SaveCollection
    {
        public string key;
        public List<string> values;

        public SaveCollection()
        {
            values = new List<string>();
        }

        public SaveCollection(string key) : this()
        {
            this.key = key;
        }

        public SaveCollection(string key, List<string> values) : this(key)
        {
            this.values = values;
        }
        
        public override string ToString()
        {
            return $"[{key} Count:{values.Count}]" + base.ToString();
        }
    }
}
