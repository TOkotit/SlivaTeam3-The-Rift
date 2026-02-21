using System.Collections.Generic;
using Entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity
{
    public class WeaponProfile : ScriptableObject
    {
        [SerializeField] private float range;
        [SerializeField] private int damage;
        [SerializeField] private bool piercing; 
        [SerializeField] private float attackSpeed;
        [SerializeField] private float swingSpeed;
        public float Range => range;
        public int Damage => damage;
        public bool Piercing => piercing;
        public float SwingSpeed => swingSpeed;
        private Dictionary<List<Key>, IAttackProfile> _attacks = new Dictionary<List<Key>, IAttackProfile>();
        public Dictionary<List<Key>, IAttackProfile>  Attacks => _attacks;
    }
}