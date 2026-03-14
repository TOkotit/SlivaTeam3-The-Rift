using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity.Attacks;
using Enums;
using Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
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
        private Dictionary<InputButton, ButtonState> _buttonStates = new Dictionary<InputButton, ButtonState>();

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

        private void OnEnable()
        {
            foreach (var state in _buttonStates.Values)
                state.Subscribe(OnButtonStarted, OnButtonCanceled);
        }

        private void OnDisable()
        {
            foreach (var state in _buttonStates.Values)
                state.Unsubscribe();
            StopAllHoldCoroutines();
        }

        private void OnDestroy()
        {
            foreach (var state in _buttonStates.Values)
                state.Unsubscribe();
        }

        private void RebuildComboData()
        {
            foreach (var state in _buttonStates.Values)
                state.Unsubscribe();
            _buttonStates.Clear();

            StopAllHoldCoroutines();

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

            foreach (var button in _availableComboKeys)
            {
                var action = FindInputAction(button);
                if (action != null)
                {
                    var state = new ButtonState(button, action);
                    state.Subscribe(OnButtonStarted, OnButtonCanceled);
                    _buttonStates[button] = state;
                }
                else
                {
                    Debug.LogWarning($"InputAction for button {button} not found.");
                }
            }
        }

        private InputAction FindInputAction(InputButton button)
        {
            string actionName = button.device == InputButton.DeviceType.Keyboard
                ? button.key.ToString()
                : button.mouse == MouseButton.Left ? "LeftMouse" : "RightMouse";

            var gameplayMap = _inputManager.GameInput.asset.FindActionMap("Gameplay");
            if (gameplayMap == null)
            {
                return null;
            }
            return gameplayMap.FindAction(actionName);
        }

        private void StopAllHoldCoroutines()
        {
            foreach (var state in _buttonStates.Values)
            {
                if (state.HoldCoroutine != null)
                {
                    _coroutineRunner.StopRoutine(state.HoldCoroutine);
                    state.HoldCoroutine = null;
                }
                state.HoldTriggered = false;
            }
        }

        private void OnButtonStarted(InputButton button)
        {
            if (!_inputAvailable) return;
            if (!_buttonStates.TryGetValue(button, out var state)) return;

            if (button.hold)
            {
                if (state.HoldCoroutine == null)
                {
                    state.HoldCoroutine = _coroutineRunner.StartRoutine(HoldTimer(state));
                }
            }
        }

        private void OnButtonCanceled(InputButton button)
        {
            if (!_inputAvailable) return;
            if (!_buttonStates.TryGetValue(button, out var state)) return;

            if (button.hold)
            {
                if (state.HoldCoroutine != null)
                {
                    _coroutineRunner.StopRoutine(state.HoldCoroutine);
                    state.HoldCoroutine = null;
                }
                state.HoldTriggered = false;
            }
            else
            {
                TryAddKey(button);
            }
        }

        private IEnumerator HoldTimer(ButtonState state)
        {
            yield return new WaitForSeconds(state.Button.treshold);

            if (state.Action.IsPressed() && !state.HoldTriggered)
            {
                state.HoldTriggered = true;
                TryAddKey(state.Button);
            }

            state.HoldCoroutine = null;
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
                Debug.Log(weapon.Durability);
                if (weapon.Durability <= 0)
                {
                    _mainCharacter.MainCharacterModel.Weapons.Remove(weapon.Model);
                    _equippedWeapons.Remove(weapon);
                    RebuildComboData();
                }
                _inputAvailable = false;
                yield return new WaitForSeconds(Math.Max(0, _bindToPerform.AttackProfile.Value.Cooldown - _comboTimeout));
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

        public void AddWeapon(Weapon weapon)
        {
            _equippedWeapons.Add(weapon);
            _mainCharacter.MainCharacterModel.Weapons.Add(weapon.Model);
            RebuildComboData();
        }
    }
}