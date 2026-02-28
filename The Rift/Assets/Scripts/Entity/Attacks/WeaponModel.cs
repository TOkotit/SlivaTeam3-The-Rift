using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Attacks
{
    public class WeaponModel
    {
        private float _range;
        private int _damage;
        private bool _piercing; 
        private float _attackSpeed;
        private float _swingSpeed;
        private string _name;
        private int _maxDurability;
        private int _currentDurability;
        private Dictionary<Key,string> _attackIDs;
        
        public float Range => _range;
        public int Damage => _damage;
        public bool Piercing => _piercing;
        public float AttackSpeed => _attackSpeed;
        public float SwingSpeed => _swingSpeed;
        public string Name => _name;
        public int MaxDurability => _maxDurability;

        public int CurrentDurability
        {
            get => _currentDurability;
            set
            {
                _currentDurability = Math.Clamp(value, 0, _maxDurability);
            }
        }


        public WeaponModel(WeaponProfile profile)
        {
            _range = profile.Range;
            _damage = profile.Damage;
            _piercing = profile.Piercing;
            _attackSpeed = profile.AttackSpeed;
            _swingSpeed = profile.SwingSpeed;
            _name = profile.Name;
            _maxDurability = profile.MaxDurability;
            _currentDurability = _maxDurability;
        }
    }
}