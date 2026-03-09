using System;
using System.Collections.Generic;
using Entity.Runes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Attacks
{
    public class WeaponModel
    {
        private float _range;
        private float _damage;
        private bool _piercing; 
        private float _attackSpeed;
        private float _swingSpeed;
        private string _name;
        private int _maxDurability;
        private float _currentDurability;
        private Dictionary<Key,string> _attackIDs;
        
        //----TOkotit: Добавляю руны и кэш с рунами
        private readonly Dictionary<Influence, float> _multipliersCache = new();
        private bool _isDirty = true;
        // Руны, по хорошему потом будем сохранять их
        private readonly List<RuneData> _runes = new();
        //---------

        
        public float Range => _range;
        public float Damage => _damage * GetMultiplier(Influence.Damage);
        public bool Piercing => _piercing;
        public float AttackSpeed => _attackSpeed;
        public float SwingSpeed => _swingSpeed;
        public string Name => _name;
        public int MaxDurability => _maxDurability;

        public float CurrentDurability
        {
            get => Math.Clamp(_currentDurability *  GetMultiplier(Influence.Durability), 0, _maxDurability);
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
            Debug.Log($"Runes on weapon:");
            foreach (var rune in _runes)
            {
                Debug.Log(rune);
            }
        }
        
        // Должно работать, но я не проверял потому что хз как
        public void AddRune(RuneData rune)
        {
            _runes.Add(rune);
            _isDirty = true;
        }

        private float GetMultiplier(Influence influence)
        {
            if (_isDirty) RefreshCache();
            return _multipliersCache.GetValueOrDefault(influence, 1f);
        }

        private void RefreshCache()
        {
            _multipliersCache.Clear();
            foreach (Influence influence in Enum.GetValues(typeof(Influence)))
                _multipliersCache[influence] = RuneCalculator.GetTotalMultiplier(_runes, influence);
            
            _isDirty = false;
        }
        
    }
}