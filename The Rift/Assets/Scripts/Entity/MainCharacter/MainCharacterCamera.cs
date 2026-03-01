using System;
using Systems;
using UnityEngine;
using VContainer;

namespace MainCharacter
{
    [RequireComponent(typeof(Camera))]
    public class MainCharacterCamera : MonoBehaviour
    {
        public Camera _camera;
        private GameInput _gameInput;
        private IControllable _controllable;
        private bool _initialized;
        [SerializeField] private float _sensitivity = 1f; 
        [SerializeField] private float _maxPitch = 80f;  
        private Vector2 _rotation; 

        private void Awake()
        { 
            _camera = GetComponent<Camera>();
        }
        
        [Inject]
        public void Construct(IGameInputManager gameInputManager, IControllable controllable)
        {
            _gameInput = gameInputManager.GameInput;
            _controllable = controllable;
            _initialized = true;
        }

        private void Update()
        {
            if (!_initialized) return;
            ReadRotation();
        }

        private void ReadRotation()
        {
            var inputDelta = _gameInput.Gameplay.MouseDrag.ReadValue<Vector2>();
            RotateInternal(inputDelta);
            var cameraInput = new Vector3(0, inputDelta.x, 0);
            Quaternion characterRotation = Quaternion.Euler(0f, _rotation.y, 0f);
            _controllable.Rotate(characterRotation);
        }

        private void RotateInternal(Vector2 inputDelta)
        {
            _rotation.x -= inputDelta.y * _sensitivity; 
            _rotation.y += inputDelta.x * _sensitivity; 
            _rotation.x = Mathf.Clamp(_rotation.x, -_maxPitch, _maxPitch);
            transform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, 0f);
        }
    }
}