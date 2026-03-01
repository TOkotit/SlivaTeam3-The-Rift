using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity.Attacks;
using Enums;
using Systems;
using UnityEngine;
using Utils;
using Utils.MiscClasses;
using VContainer;

namespace MainCharacter
{
    public class MainCharacterAttackController : MonoBehaviour
    {
        [Inject] private IGameInputManager _inputManager;
        [Inject] private AttackSystem _attackSystem;
        [Inject] private ICoroutineRunner _coroutineRunner;
        [Inject] private WeaponManager _weaponManager;

        [SerializeField] private float _comboTimeout = 0.1f;
        [SerializeField] private MainCharacter _mainCharacter;

        private LimitedQueue<InputButton> _inputs = new LimitedQueue<InputButton>(4);
        private HashSet<InputButton> _availableComboKeys = new HashSet<InputButton>();
        private List<Weapon> _equippedWeapons = new List<Weapon>();
        private List<AttackBind> _allBinds = new List<AttackBind>();
        private Dictionary<InputButton, float> _holdStartTimes = new Dictionary<InputButton, float>();
        private HashSet<InputButton> _holdEventsFired = new HashSet<InputButton>();

        private Coroutine _timeoutCoroutine;
        private bool _inputAvailable = true;
        private AttackBind _bindToPerform;

        public List<Weapon> EquippedWeapons
        {
            get => _equippedWeapons;
            set
            {
                _equippedWeapons = value;
                RebuildComboData();
            }
        }

        private void Awake()
        {
            RebuildComboData();
        }

        private void RebuildComboData()
        {
            _availableComboKeys.Clear();
            _allBinds.Clear();

            foreach (var weapon in _equippedWeapons)
            {
                foreach (var bind in weapon.AttackBinds)
                {
                    _allBinds.Add(bind);
                    foreach (var key in bind.keys)
                    {
                        _availableComboKeys.Add(key);
                    }
                }
            }
        }

        public void AddWeapon(Weapon weapon)
        {
            _equippedWeapons.Add(weapon);
            _mainCharacter.MainCharacterModel.Weapons.Add(weapon.Model);
            RebuildComboData();
        }

        

        private void Update()
        {
            if (!_inputAvailable) return;
            
            foreach (var button in _availableComboKeys)
            {
                if (button.hold)
                {
                    var isHeld = button.IsHeld();
                    
                    if (isHeld)
                    {
                        if (!_holdStartTimes.ContainsKey(button))
                        {
                            _holdStartTimes[button] = Time.time;
                        }
                        else
                        {
                            float elapsed = Time.time - _holdStartTimes[button];
                            if (elapsed >= button.treshold && !_holdEventsFired.Contains(button))
                            {
                                TryAddKey(button);
                                _holdEventsFired.Add(button);
                            }
                        }
                    }
                    else
                    {
                        if (_holdStartTimes.ContainsKey(button))
                        {
                            _holdStartTimes.Remove(button);
                            _holdEventsFired.Remove(button);
                        }
                    }
                }
                else if (button.IsPressed() && !button.IsHeld())
                {
                    TryAddKey(button);
                }
            }
        }

        private void TryAddKey(InputButton key)
        {
            var newSequence = _inputs.Concat(new[] { key }).ToArray();
            var isValidPrefix = _allBinds.Any(bind => IsPrefix(newSequence, bind.keys.ToArray()));

            if (!isValidPrefix)
            {
                return;
            }

            _inputs.Enqueue(key);

            var exactMatch = _allBinds.FirstOrDefault(bind => bind.keys.SequenceEqual(_inputs));
            if (exactMatch != null)
            {
                _bindToPerform = exactMatch;
            }

            if (_timeoutCoroutine == null)
            {
                _timeoutCoroutine = _coroutineRunner.StartRoutine(InputTimeout());
            }
        }

        private IEnumerator InputTimeout()
        {
            yield return new WaitForSeconds(_comboTimeout);

            if (_bindToPerform != null)
            {
                _attackSystem.PerformAttack(_bindToPerform.AttackProfile.Value, _bindToPerform.weapon, gameObject, Teams.Player);
                var weapon = _bindToPerform.weapon;
                weapon.Damage(1);
                if(weapon.Durability <= 0)
                {
                    _mainCharacter.MainCharacterModel.Weapons.Remove(weapon.Model);
                    _equippedWeapons.Remove(weapon);
                    RebuildComboData();    
                }
                _inputAvailable = false;
                yield return new WaitForSeconds(Math.Max(0,_bindToPerform.AttackProfile.Value.Cooldown - _comboTimeout));
                _inputAvailable = true;
            }

            ResetInput();
        }

        private void ResetInput()
        {
            _inputs.Clear();
            _bindToPerform = null;

            if (_timeoutCoroutine != null)
            {
                _coroutineRunner.StopRoutine(_timeoutCoroutine);
                _timeoutCoroutine = null;
            }
        }

        private static bool IsPrefix<T>(IEnumerable<T> prefix, IEnumerable<T> full)
        {
            using var prefixEnum = prefix.GetEnumerator();
            using var fullEnum = full.GetEnumerator();

            while (prefixEnum.MoveNext())
            {
                if (!fullEnum.MoveNext()) return false;
                if (!Equals(prefixEnum.Current, fullEnum.Current)) return false;
            }
            return true;
        }
    }
}