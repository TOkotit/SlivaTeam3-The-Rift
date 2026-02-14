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
        [Inject] private MainCharacterCamera _mainCamera;

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

            var cameraTransform = _mainCamera.transform;
            var forward = cameraTransform.forward;
            var right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            var moveDirection = forward * inputDirection.y + right * inputDirection.x;

            _controllable.Move(moveDirection);
        }
        private void OnDisable()
        {
            _gameInput.Gameplay.Jump.performed -= OnJumpPerformed;
        }
    }
}