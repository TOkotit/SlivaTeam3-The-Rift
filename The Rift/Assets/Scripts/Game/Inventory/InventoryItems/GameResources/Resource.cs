using Game.Inventory.InventoryItems;
using R3;

namespace Game.Inventory
{
    /// <summary>
    /// Реактивная оболочка над ресурсами чтобы при изменении количества обновлять UI и тд
    /// </summary>
    public class Resource : Item
    {
        public new ResourceData Origin => (ResourceData)base.Origin;
        public ResourceType ResourceType => Origin.resourceType;

        public Resource(ResourceData origin) : base(origin)
        {
        }
    }
}