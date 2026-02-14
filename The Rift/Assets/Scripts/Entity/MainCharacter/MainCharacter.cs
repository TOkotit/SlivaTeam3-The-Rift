using System;
using Systems;
using UnityEngine;
using VContainer;

namespace MainCharacter
{
    public class MainCharacter : MonoBehaviour
    {
        [Inject] private MainCharacterModel _mainCharacterModel;
        [Inject] private CharacterController _characterController;

    }
}