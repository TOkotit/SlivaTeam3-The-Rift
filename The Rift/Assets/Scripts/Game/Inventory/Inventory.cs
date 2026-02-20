using System.Collections.Generic;
using Game.Gameplay;
using ObservableCollections;
using R3;
using UnityEditor;
using UnityEngine;


namespace Game.Inventory
{
    /// <summary>
    /// Реактивная обертка над инвентарем
    /// </summary>
    public class Inventory
    {
        private readonly InventoryModel _inventoryModel;

        public ObservableList<Resource> Resources;
        
        public Inventory(GameData gameData)
        {
            _inventoryModel = gameData._inventoryModel;
            Resources = new ObservableList<Resource>();
            
            //Связываем этот инвентарь и чистый инвентарь с данными
            foreach (var resourceData in gameData._inventoryModel.resources)
            {
                Resources.Add(new Resource(resourceData));
            }

            Resources.ObserveAdd().Subscribe(e =>
            {
                var added = e.Value;
                _inventoryModel.resources.Add(added.Origin);
                Debug.Log($"Added {e.Value}");
            });
            
            Resources.ObserveRemove().Subscribe(e =>
            {
                var removed = e.Value;
                _inventoryModel.resources.Remove(removed.Origin);
                Debug.Log($"Removed {e.Value}");
            });

        }
    }
}