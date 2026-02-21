using System;
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
        public override DamagableModel Damagable => _mainCharacterModel;
        public MainCharacterModel MainCharacterModel => _mainCharacterModel;
        [SerializeField] private GameObject arms; 
    }
}