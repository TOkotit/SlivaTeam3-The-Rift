using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Enums;
using Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.MiscClasses;
using VContainer;

namespace MainCharacter
{
    public class MainCharacterAttackController : MonoBehaviour
    {
        [Inject] private GameInputManager _inputManager;
        [Inject] private AttackSystem _attackSystem;

        [SerializeField] private float _comboTimeout = 0.5f; 

        private LimitedQueue<Key> _inputs = new LimitedQueue<Key>(4);
        private HashSet<Key> _availableComboKeys = new HashSet<Key>();
        private List<WeaponProfile> _equippedWeapons = new List<WeaponProfile>();

        private struct ComboEntry
        {
            public Key[] Keys; 
            public IAttackProfile AttackProfile;
            public WeaponProfile Weapon; 
        }
        private List<ComboEntry> _allCombos = new List<ComboEntry>();

        private Coroutine _timeoutCoroutine;
        private bool _waitingForNextInput;

        public List<WeaponProfile> EquippedWeapons
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
            _allCombos.Clear();

            foreach (var weapon in _equippedWeapons)
            {
                foreach (var kvp in weapon.Attacks) 
                {
                    var keys = kvp.Key.ToArray(); 
                    _allCombos.Add(new ComboEntry{ Keys = keys, AttackProfile = kvp.Value, Weapon = weapon });
                    
                    foreach (var key in keys)
                    {
                        _availableComboKeys.Add(key);
                    }
                }
            }
        }

        private void Update()
        {
            foreach (var key in _availableComboKeys)
            {
                if (Keyboard.current[key].wasPressedThisFrame)
                {
                    _inputs.Enqueue(key);

                    if (_timeoutCoroutine != null)
                        StopCoroutine(_timeoutCoroutine);

                    _timeoutCoroutine = StartCoroutine(InputTimeout());

                    CheckForCombo();

                    break;
                }
            }
        }

        private void CheckForCombo()
        {
            var currentSequence = _inputs.ToArray(); 
            foreach (var combo in _allCombos)
            {
                if (IsSequenceMatch(currentSequence, combo.Keys))
                {
                    _attackSystem.PerformAttack(combo.AttackProfile, combo.Weapon, gameObject, Teams.Player );

                    _inputs.Clear();
                    if (_timeoutCoroutine != null)
                    {
                        StopCoroutine(_timeoutCoroutine);
                        _timeoutCoroutine = null;
                    }
                    return; 
                }
            }
        }

        private bool IsSequenceMatch(Key[] actual, Key[] expected)
        {
            if (actual.Length != expected.Length)
                return false;

            for (var i = 0; i < actual.Length; i++)
            {
                if (actual[i] != expected[i])
                    return false;
            }
            return true;
        }

        private IEnumerator InputTimeout()
        {
            yield return new WaitForSeconds(_comboTimeout);
            _inputs.Clear();
            _timeoutCoroutine = null;
        }
    }
}