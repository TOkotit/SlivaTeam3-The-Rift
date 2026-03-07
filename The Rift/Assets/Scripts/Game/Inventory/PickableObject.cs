using System;
using Enums;
using UnityEngine;
using NaughtyAttributes;

namespace Game.Inventory
{
    public class PickableObject : MonoBehaviour
    {
        public ItemsCategory category;
    
        [ShowIf("category", ItemsCategory.Resource)]
        public ResourceType resourceType;
        
        
        [Header("Settings")]
        [SerializeField] private float _flySpeed = 5f; 
        [SerializeField] private float _acceleration = 10f; 
        [SerializeField] private float _collectDistance = 0.5f;
        
        
        private Transform _target;
        private InventoryManager _inventoryManager;
        private bool _isBeingCollected;
        
        
        public void StartPickup(Transform target, InventoryManager manager)
        {
            if (_isBeingCollected) return;

            _target = target;
            _inventoryManager = manager;
            _isBeingCollected = true;

            if (TryGetComponent<Collider>(out var col)) col.enabled = false;
        }

        private void Update()
        {
            if (!_isBeingCollected || !_target) return;

            _flySpeed += _acceleration * Time.deltaTime;

            transform.position = Vector3.MoveTowards(
                transform.position, 
                _target.position, 
                _flySpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, _target.position) < _collectDistance)
            {
                FinalizePickup();
            }
        }
        
        private void FinalizePickup()
        {
            if (category == ItemsCategory.Resource)
                _inventoryManager.AddResource(resourceType, 1);
            else 
                _inventoryManager.AddItem(category, 1);

            Destroy(gameObject);
        }
        
        private void OnValidate()
        {
            if (category != ItemsCategory.Resource)
            {
                resourceType = ResourceType.None;
            }
        }
    }
}