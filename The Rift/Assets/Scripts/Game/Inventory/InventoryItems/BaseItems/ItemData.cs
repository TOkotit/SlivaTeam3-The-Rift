using System;
using Enums;

namespace Game.Inventory.InventoryItems
{
    /// <summary>
    /// Для хранения инфы о ресурсе
    /// </summary>
    [Serializable]
    public class ItemData
    {
        public ItemsCategory itemsCategory; 
        public readonly string name;
        public readonly string description;
        public readonly int cost;
        public  int amount;
    }
}