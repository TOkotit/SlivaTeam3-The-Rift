using System;
using System.Collections.Generic;
using Entity.Runes;
using Game.Inventory.Runes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Attacks
{
    public class WeaponModel
    {
        
        private float _lastHitTime = -9999f; // отслеживание времени с попадания, нужно для эффекта от руны временного
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
        private bool _isDirty = true;
        // Руны, по хорошему потом будем сохранять их
        private readonly List<RuneData> _runes = new();
        //---------

        
        public float Range => _range;
        public float Damage => _damage * GetMultiplier(Influence.Damage);
        public bool Piercing => _piercing;

        public float AttackSpeed
        {
            get => _attackSpeed * GetMultiplier(Influence.Cooldown);
            set => _attackSpeed = value;
        }
        public float SwingSpeed => _swingSpeed;
        public string Name => _name;
        public int MaxDurability => _maxDurability;

        public float CurrentDurability
        {
            get => Math.Clamp(_currentDurability * GetMultiplier(Influence.Durability), 0, _maxDurability);
            set => _currentDurability = Math.Clamp(value, 0, _maxDurability);
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
        
        
        public void RegisterHit()
        {
            _lastHitTime = Time.time;
        }
        
        public void AddRune(RuneData rune) => _runes.Add(rune);

        private float GetMultiplier(Influence influence)
        {
            var context = new RuneContext 
            { 
                CurrentDurabilityPercent = _maxDurability > 0 ? _currentDurability / _maxDurability : 0,
                TimeSinceLastHit = Time.time - _lastHitTime
            };
    
            return RuneCalculator.GetTotalMultiplier(_runes, influence, context);
        }
    }
}