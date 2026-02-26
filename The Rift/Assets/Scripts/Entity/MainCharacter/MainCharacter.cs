using System;
using System.Collections.Generic;
using Entity;
using Entity.Attacks;
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
        [Inject] private WeaponManager _weaponManager;
        public override DamagableModel Damagable => _mainCharacterModel;
        public MainCharacterModel MainCharacterModel => _mainCharacterModel;
        [SerializeField] private GameObject arms;
        [SerializeField] private string weaponID; //Свойство для теста, потом переделать получение через инвентарь
        public GameObject Arms => arms;

        private void Start()
        {
            _attackController.AddWeapon(_weaponManager.CreateWeapon(weaponID));
            Debug.Log(_mainCharacterModel.Weapons.Count + " weapons have been equipped");
        }
    }
}