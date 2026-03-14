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
        private MainCharacterModel _mainCharacterModel;
        [Inject] private CharacterController _characterController;
        [Inject] private MainCharacterAttackController _attackController;
        [Inject] private WeaponManager _weaponManager;

        [Inject]
        private void SetupModel(Stamina stamina, Health health, MovementStatsSO stats, MainCharacterModel mainCharacterModel)
        {
            _mainCharacterModel = mainCharacterModel;
            mainCharacterModel.Stamina = stamina;
            mainCharacterModel.Health = health;
            
            _mainCharacterModel.DashCooldown = stats.DashCooldown;
            _mainCharacterModel.DashSpeed = stats.DashSpeed;
            _mainCharacterModel.DashTime = stats.DashTime;
            _mainCharacterModel.DashCost = stats.DashCost;
            _mainCharacterModel.WallJumpCount = stats.WallJumpCount;
            _mainCharacterModel.WallJumpCost = stats.@WallJumpCost;
            _mainCharacterModel.Speed = stats.Speed;
            _mainCharacterModel.JumpHeight = stats.JumpHeight;
        }
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