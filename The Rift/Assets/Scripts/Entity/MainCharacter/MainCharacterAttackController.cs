using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.MiscClasses;
using VContainer;

namespace MainCharacter
{
    public class MainCharacterAttackController : MonoBehaviour
    {
        [Inject]
        private GameInputManager  _inputManager;

        private LimitedQueue<Key> inputs = new LimitedQueue<Key>(4);
        private HashSet<Key> _availableComboKeys;
        private List<WeaponProfile> _equippedWeapons;

        public List<WeaponProfile> EquippedWeapons
        {
            get { return _equippedWeapons; }
            set
            {
                _equippedWeapons = value;
                foreach (var weapon in _equippedWeapons)
                {
                    foreach (var attack in weapon.Attacks.Keys)
                    {
                        foreach (var key in attack)
                        {
                            _availableComboKeys.Add(key);
                        }
                    }
                }
            }
        } 
        
        private void Update()
        {
            
        }
    }
}