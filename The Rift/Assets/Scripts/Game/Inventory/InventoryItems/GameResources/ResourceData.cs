using System;
using Game.Inventory.InventoryItems;

namespace Game.Inventory
{
    /// <summary>
    /// Для хранения инфы о ресурсе
    /// </summary>
    [Serializable]
    public class ResourceData : ItemData
    {
        public ResourceType resourceType;
    }
}