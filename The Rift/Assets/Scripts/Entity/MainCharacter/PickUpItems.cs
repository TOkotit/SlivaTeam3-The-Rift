using Enums;
using Game.Inventory;
using UnityEngine;
using VContainer;

namespace MainCharacter
{
    public class PickUpItems : MonoBehaviour
    {
        
        [Inject] private InventoryManager _inventoryManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PickableItem"))
            {
                var pickableObject = other.GetComponent<PickableObject>();
                
                if (pickableObject)
                {
                    pickableObject.StartPickup(transform, _inventoryManager);
                }
            }
        }
      
    }
}