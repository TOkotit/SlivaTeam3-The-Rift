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
        
        // слоты для рун
        private List<RuneSlot> _runeSlots;
        // Руны, по хорошему потом будем сохранять их
        public readonly List<RuneData> _runes = new();
        //---------
        public List<RuneSlot> Slots => _runeSlots;
        
        public float Range => _range;
        public float Damage => RuneCalculator.CalculateStat(_damage, Influence.Damage, _runeSlots, CreateContext);
        
        public bool Piercing => _piercing;

        public float AttackSpeed
        {
            get => RuneCalculator.CalculateStat(_attackSpeed, Influence.Cooldown, _runeSlots, CreateContext);
            set => _attackSpeed = value;
        }
        public float SwingSpeed => _swingSpeed;
        public string Name => _name;
        public int MaxDurability => _maxDurability;

        public float CurrentDurability
        {
            get 
            {
                var maxWithRunes = RuneCalculator.CalculateStat(_maxDurability, Influence.Durability, _runeSlots, CreateContext);
                return Math.Clamp(_currentDurability, 0, maxWithRunes);
            }
            set => _currentDurability = value; 
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
            _runeSlots = new ();
        }
        
        
        public void RegisterHit(GameObject target, Vector3 hitPoint)
        {
            _lastHitTime = Time.time;
            
            foreach (var slot in _runeSlots)
                if (!slot.IsEmpty)
                {
                    var context = CreateContext(slot.SlotType); 
                    context.Target = target;
                    context.HitPoint = hitPoint;
            
                    slot.EquippedRune.OnWeaponHit(context);
                }
        }
        
        private RuneContext CreateContext(RuneSlotsType slotType)
        {
            return new RuneContext 
            { 
                CurrentDurabilityPercent = _maxDurability > 0 ? _currentDurability / _maxDurability : 0,
                TimeSinceLastHit = Time.time - _lastHitTime,
                EquipType = EquipmentType.Weapon,
                CurrentSlotType = slotType,
            };
        } 
            
        
        public void AddRune(RuneData rune)
        {
            if (_runes.Contains(rune)) 
            {
                Debug.LogWarning($"Руна {rune.runeName} уже установлена на это оружие!");
                return;
            }
            
            _runes.Add(rune);
        }
        
        public bool TryInsertRune(RuneData rune, int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= _runeSlots.Count) return false;
            if (!_runeSlots[slotIndex].IsEmpty) return false;

            foreach (var slot in _runeSlots)
                if (!slot.IsEmpty && slot.EquippedRune == rune) return false;

            _runeSlots[slotIndex].EquippedRune = rune;
            return true;
        }
        
        public RuneData ExtractRune(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= _runeSlots.Count) return null;
            
            var rune = _runeSlots[slotIndex].EquippedRune;
            _runeSlots[slotIndex].EquippedRune = null;
            return rune;
        }
        
        private IEnumerable<RuneData> GetActiveRunes()
        {
            foreach (var slot in _runeSlots)
                if (!slot.IsEmpty)
                    yield return slot.EquippedRune;
        }
    }
}