using System;
using System.Collections.Generic;
using Game.Inventory.InventoryItems;
using Mono.Cecil;

namespace Game.Inventory
{
    [Serializable]
    public class InventoryModel
    {
        public List<ItemData> items =  new();
        


    }
}