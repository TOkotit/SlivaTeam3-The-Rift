using Enums;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class CharacterPickUpItem : MonoBehaviour
    {
        
        [Inject] private InventoryManager _inventoryManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PickableItem")
            {
                var pickableObject = other.GetComponent<PickableObject>();
                if (pickableObject.category == ItemsCategory.Resource)
                    _inventoryManager.AddResource(pickableObject.resourceType, 1);
                else _inventoryManager.AddItem(pickableObject.category, 1);
                
                Destroy(other.gameObject);
                
            }
        }
    }
}