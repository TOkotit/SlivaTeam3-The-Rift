using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class DamagableRegistry
    {
        private Dictionary<Collider, DamagableModel> _damagableObjects = new Dictionary<Collider, DamagableModel>();
        
        public void Register(Character character)
        {
            var colliders = character.GetComponentsInChildren<Collider>();
            foreach (var col in colliders)
            {
                if (!_damagableObjects.ContainsKey(col))
                    _damagableObjects.Add(col, character.Damagable); 
            }
        }
        
        
        public void Unregister(Character character)
        {
            var colliders = character.GetComponentsInChildren<Collider>();
            foreach (var col in colliders)
            {
                if (_damagableObjects.ContainsKey(col))
                    _damagableObjects.Remove(col);
            }
        }

        public DamagableModel TryGetCharacter(Collider collider)
        {
            if (_damagableObjects.ContainsKey(collider))
            {
                return _damagableObjects[collider];
            }
            return null;
        }
    }
}