using System;
using System.Collections.Generic;
using Game.Inventory.InventoryItems;

namespace Game.Inventory
{
    [Serializable]
    public class InventoryModel
    {
        public List<ItemData> items =  new();
        


    }
}