using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Data/Inventory Data")]
    [Serializable]
    public class InventoryDataSO : ScriptableObject
    {
        public List<ItemDataSO> itemDatas = new List<ItemDataSO>();
    }
}
