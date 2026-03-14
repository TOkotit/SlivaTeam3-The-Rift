using System.Collections.Generic;
using Game.Gameplay;
using Game.Inventory.InventoryItems;
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

        public ObservableList<Item> Items;
        
        public Inventory(GameData gameData)
        {
            _inventoryModel = gameData._inventoryModel;
            Items = new ObservableList<Item>();
            
            //Связываем этот инвентарь и чистый инвентарь с данными
            foreach (var itemsData in gameData._inventoryModel.items)
            {
                Items.Add(new Item(itemsData));
            }

            Items.ObserveAdd().Subscribe(e =>
            {
                var added = e.Value;
                _inventoryModel.items.Add(added.Origin);
                Debug.Log($"Added {e.Value}");
            });
            
            Items.ObserveRemove().Subscribe(e =>
            {
                var removed = e.Value;
                _inventoryModel.items.Remove(removed.Origin);
                Debug.Log($"Removed {e.Value}");
            });

        }
    }
}