using System;
using Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace MainCharacter
{
    public class MainCharacterInputController : MonoBehaviour
    {
        private IControllable _controllable;
        private IGameInputManager _gameInputManager;
        private GameInput _gameInput;

        [Inject]
        private void Construct(IGameInputManager gameInputManager)
        {
            _gameInputManager = gameInputManager;
            _gameInput = gameInputManager.GameInput;
        }
        private void Awake()
        {
            _controllable = GetComponent<IControllable>();
            if (_controllable == null)
            {
                Debug.Log("Main character controllable is not found");
            }
        }
        private void OnEnable()
        {
            _gameInput.Gameplay.Jump.performed += OnJumpPerformed;
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            _controllable.Jump();
        }

        private void Update()
        {
            ReadMovement();
        }

        private void ReadMovement()
        {
            var inputDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
            _controllable.Move(moveDirection);
        }
        private void OnDisable()
        {
            _gameInput.Gameplay.Jump.performed -= OnJumpPerformed;
        }
    }
}