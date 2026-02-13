using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace MainCharacter
{
    public class MainCharacterInputController : MonoBehaviour
    {
        private IControllable _controlable;
        private GameInput _gameInput;

        private void Awake()
        {
            _controlable = GetComponent<IControllable>();
            if (_controlable == null)
            {
                Debug.Log("Main character controllable is not found");
            }
        }
        [Inject]
        public void Construct(GameInput gameInput)
        {
            _gameInput = gameInput;
        }
        
        private void OnEnable()
        {
            _gameInput.Gameplay.Jump.performed += OnJumpPerformed;
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            _controlable.Jump();
        }

        private void Update()
        {
            ReadMovement();
        }

        private void ReadMovement()
        {
            var inputDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
            _controlable.Move(inputDirection);
        }
        private void OnDisable()
        {
            _gameInput.Gameplay.Jump.performed -= OnJumpPerformed;
        }
    }
}