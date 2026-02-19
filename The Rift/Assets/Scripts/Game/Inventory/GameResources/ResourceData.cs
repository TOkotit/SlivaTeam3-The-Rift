using System;

namespace Game.Inventory
{
    /// <summary>
    /// Для хранения инфы о ресурсе
    /// </summary>
    [Serializable]
    public class ResourceData
    {
        public ResourceType resourceType;
        public int amount;
    }
}