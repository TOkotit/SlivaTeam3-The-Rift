using System.Collections.Generic;
using Entity;
using Entity.Attacks;
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
        [SerializeField] private int maxDurability;
        public float Range => range;
        public int Damage => damage;
        public bool Piercing => piercing; 
        public float SwingSpeed => swingSpeed;
        public int MaxDurability => maxDurability;
        public string Name => name;
        public float AttackSpeed => attackSpeed;
        
        [SerializeField]
        public List<AttackBind> _attacks = new List<AttackBind>();
        
        
        
    }
}