using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Attacks
{
    public class WeaponModel
    {
        private float range;
        private int damage;
        private bool piercing; 
        private float attackSpeed;
        private float swingSpeed;
        private string name;
        private int maxDurability;
        private int currentDurability;
        
        public float Range => range;
        public int Damage => damage;
        public bool Piercing => piercing;
        public float AttackSpeed => attackSpeed;
        public float SwingSpeed => swingSpeed;
        public string Name => name;
        public int MaxDurability => maxDurability;
        public int CurrentDurability => currentDurability;
        
        private List<AttackBind> _attacks = new List<AttackBind>();
        public IReadOnlyList<AttackBind> AttackBinds => _attacks;

        public WeaponModel(WeaponProfile profile)
        {
            range = profile.Range;
            damage = profile.Damage;
            piercing = profile.Piercing;
            attackSpeed = profile.AttackSpeed;
            swingSpeed = profile.SwingSpeed;
            name = profile.Name;
            maxDurability = profile.MaxDurability;
            currentDurability = maxDurability;
        }
    }
}