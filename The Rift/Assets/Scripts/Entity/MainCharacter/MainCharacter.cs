using System;
using System.Collections.Generic;
using Entity;
using Systems;
using UnityEngine;
using VContainer;

namespace MainCharacter
{
    public class MainCharacter : Character
    {
        [Inject] private MainCharacterModel _mainCharacterModel;
        [Inject] private CharacterController _characterController;
        [Inject] private MainCharacterAttackController _attackController;
        public override DamagableModel Damagable => _mainCharacterModel;
        public MainCharacterModel MainCharacterModel => _mainCharacterModel;
        [SerializeField] private GameObject arms;
        [SerializeField] private WeaponProfile weapon; //Свойство для теста, потом переделать получение через инвентарь
        public GameObject Arms => arms;

        private void Start()
        {
            _mainCharacterModel.Weapons = new List<WeaponProfile> { weapon };
            _attackController.EquippedWeapons = _mainCharacterModel.Weapons;
        }
    }
}