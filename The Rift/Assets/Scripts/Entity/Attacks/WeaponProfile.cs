using System.Collections.Generic;
using Entity;
using MainCharacter;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity
{
    
    [CreateAssetMenu(fileName = "TestWeapon", menuName = "Weapons/Melee Weapon")]
    public class WeaponProfile : ScriptableObject
    {
        [SerializeField] private float range;
        [SerializeField] private int damage;
        [SerializeField] private bool piercing; 
        [SerializeField] private float attackSpeed;
        [SerializeField] private float swingSpeed;
        [SerializeField] private string name;
        public float Range => range;
        public int Damage => damage;
        public bool Piercing => piercing; 
        public float SwingSpeed => swingSpeed;
        [SerializeField]
        public List<Attack> _attacks = new List<Attack>();
        
        [System.Serializable]
        public class Attack
        {
            public List<Key> keys;
            public RaycastAttackProfile profile; 
        }
        
        
    }
}