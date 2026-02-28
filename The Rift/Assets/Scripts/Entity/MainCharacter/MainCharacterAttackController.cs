using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Entity.Attacks;
using Enums;
using Game.Gameplay.View.UI;
using Systems;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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
        
        [SerializeField] private float _comboTimeout = 0.5f; 
        [SerializeField] MainCharacter _mainCharacter;

        private LimitedQueue<Key> _inputs = new LimitedQueue<Key>(4);
        private HashSet<Key> _availableComboKeys = new HashSet<Key>();
        private List<Weapon> _equippedWeapons = new List<Weapon>();

        private struct ComboEntry
        {
            public Key[] Keys; 
            public IAttackProfile AttackProfile;
            public Weapon Weapon; 
        }
        private List<ComboEntry> _allCombos = new List<ComboEntry>();

        private Coroutine _timeoutCoroutine;
        private bool _waitingForNextInput;

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
            Debug.Log("Awake ===============");
            RebuildComboData();
        }

        private void RebuildComboData()
        {
            _availableComboKeys.Clear();
            _allCombos.Clear();
            
            Debug.Log(_equippedWeapons.Count + " weapons have been equipped in controller");
            foreach (var weapon in _equippedWeapons)
            {

                foreach (var kvp in weapon.AttackBinds) 
                {
                    var keys = kvp.keys.ToArray(); 
                    _allCombos.Add(new ComboEntry{ Keys = keys, AttackProfile = kvp.profile, Weapon = weapon });
                    
                    foreach (var key in keys)
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
            foreach (var key in _availableComboKeys)
            {
                if (Keyboard.current[key].wasPressedThisFrame)
                {
                    _inputs.Enqueue(key);

                    if (_timeoutCoroutine != null)
                        _coroutineRunner.StopRoutine(_timeoutCoroutine);

                    _timeoutCoroutine = _coroutineRunner.StartRoutine(InputTimeout());

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
                    _attackSystem.PerformAttack(combo.AttackProfile, combo.Weapon, gameObject,Teams.Player );

                    _inputs.Clear();
                    if (_timeoutCoroutine != null)
                    {
                        _coroutineRunner.StopRoutine(_timeoutCoroutine);
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