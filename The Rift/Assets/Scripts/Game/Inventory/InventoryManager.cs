using System.Linq;
using Enums;
using Game.Inventory.InventoryItems;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryManager
    {
        public Inventory _inventory;
        
        public InventoryManager(Inventory inventory)
        {
            _inventory = inventory;
        }

        public Item CreateNewItem(ItemsCategory category)
        {
            var newIData = new ItemData()
            {
                itemsCategory = category,
                amount = 0
            };
            
            var newItem = new Item(newIData);
            
            _inventory.Items.Add(newItem);

            return newItem;
        }
        
        public void AddItem(ItemsCategory category, int amount)
        {
            var requiredItem = _inventory.Items.FirstOrDefault(r => r.ItemsCategory == category) ??
                                   CreateNewItem(category);

            requiredItem.Amount.Value += amount;
        }
        
        public bool TrySpendItems(ItemsCategory category, int amount)
        {
            var requiredItem = _inventory.Items.FirstOrDefault(r => r.ItemsCategory == category) ??
                               CreateNewItem(category);

            if (requiredItem.Amount.Value < amount)
            {
                Debug.Log($"Not enough item {category}");
                return false;
            }
            
            requiredItem.Amount.Value -= amount;
            return true;
        }
        
        public bool IsEnough(ItemsCategory category, int amount)
        {
            var requiredItem = _inventory.Items.FirstOrDefault(r => r.ItemsCategory == category) ??
                               CreateNewItem(category);

            return requiredItem.Amount.Value >= amount;
            
        }
        
        public Resource CreateNewResource(ResourceType resourceType)
        {
            var newData = new ResourceData()
            {
                itemsCategory = ItemsCategory.Resource, // Всегда Resource так как это ресурсы
                resourceType = resourceType,
                amount = 0
            };
    
            var newResource = new Resource(newData);
            _inventory.Items.Add(newResource);

            return newResource;
        }

        // Обновленный метод добавления именно ресурсов
        public void AddResource(ResourceType type, int amount)
        {
            var resource = _inventory.Items
                .OfType<Resource>() 
                .FirstOrDefault(r => r.ResourceType == type);

            if (resource == null)
            {
                resource = CreateNewResource(type);
            }
            resource.Amount.Value += amount;
        }
    }
}