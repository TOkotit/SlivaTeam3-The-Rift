using System.Linq;
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

        public Resource CreateNewResource(ResourceType resourceType)
        {
            var newRData = new ResourceData()
            {
                resourceType = resourceType,
                amount = 0
            };
            
            var newResource = new Resource(newRData);
            
            _inventory.Resources.Add(newResource);

            return newResource;
        }
        
        public void AddResource(ResourceType resourceType, int amount)
        {
            var requiredResource = _inventory.Resources.FirstOrDefault(r => r.ResourceType == resourceType) ??
                                   CreateNewResource(resourceType);

            requiredResource.Amount.Value += amount;
        }
        
        public void TrySpendResource(ResourceType resourceType, int amount)
        {
            var requiredResource = _inventory.Resources.FirstOrDefault(r => r.ResourceType == resourceType) ??
                                   CreateNewResource(resourceType);

            if (requiredResource.Amount.Value < amount)
            {
                Debug.Log($"Not enough resource {resourceType}");
                return;
            }
            
            requiredResource.Amount.Value -= amount;
        }
        
        public bool IsEnough(ResourceType resourceType, int amount)
        {
            var requiredResource = _inventory.Resources.FirstOrDefault(r => r.ResourceType == resourceType) ??
                                   CreateNewResource(resourceType);

            return requiredResource.Amount.Value >= amount;
            
        }
    }
}