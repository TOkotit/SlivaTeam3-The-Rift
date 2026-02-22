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

        private void OnValidate()
        {
            if (category != ItemsCategory.Resource)
            {
                resourceType = ResourceType.None;
            }
        }
    }
}