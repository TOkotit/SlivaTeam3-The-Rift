using System.Collections.Generic;
using ObservableCollections;
using R3;
using UnityEditor;


namespace Game.Inventory
{
    /// <summary>
    /// Реактивная обертка над инвентарем
    /// </summary>
    public class Inventory
    {
        public readonly InventoryModel _inventoryModel;

        public ObservableList<Resource> Resources;
        
        public Inventory(InventoryModel inventoryModel)
        {
            _inventoryModel = inventoryModel;
            Resources = new ObservableList<Resource>();
            
            foreach (var resourceData in inventoryModel.resources)
            {
                Resources.Add(new Resource(resourceData));
            }

            Resources.ObserveAdd().Subscribe(e =>
            {
                var added = e.Value;
                _inventoryModel.resources.Add(added.Origin);
            });
            
            Resources.ObserveRemove().Subscribe(e =>
            {
                var removed = e.Value;
                _inventoryModel.resources.Remove(removed.Origin);
            });

        }
    }
}