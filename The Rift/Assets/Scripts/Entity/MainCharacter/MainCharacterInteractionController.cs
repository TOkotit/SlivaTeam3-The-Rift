using System;
using System.Runtime.InteropServices;
using Game;
using Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace MainCharacter
{
    public class MainCharacterInteractionController : MonoBehaviour
    {
        private MainCharacterCamera _mainCharacterCamera;
        private InteractionUIManager _uiManager; 
        private IGameInputManager inputManager;
        private GameInput gameInput;
        private bool _subscribed = false;
        
        [SerializeField] private InteractionZone _zone; 
        [SerializeField] private float _sphereRadius = 0.3f;
        [SerializeField] private float _rayDistance = 4f; 
        [SerializeField] private string _interactableTag = "Interactable";

        private IInteractable _currentInteractable;

        [Inject] 
        private void Construct(IGameInputManager gameInputManager, MainCharacterCamera mainCharacterCamera, InteractionUIManager interactableUIManager)
        {
            Debug.Log("MainCharacterInteractionController.Construct called");
            inputManager = gameInputManager;
            gameInput = gameInputManager.GameInput;
            
            _mainCharacterCamera =  mainCharacterCamera;
            _uiManager = interactableUIManager;
            Debug.Log($"_gameInput is null? {gameInput == null}");
            TrySubscribe();
        }
        
        private void OnEnable()
        {
            TrySubscribe();
        }
        
        private void OnDisable()
        {
            TryUnsubscribe();
        }
        
        private void TrySubscribe()
        {
            if (_subscribed) return;
            if (gameInput == null) return;
            if (gameInput.Gameplay.Interact == null) return;
            
            gameInput.Gameplay.Interact.performed += OnInteractPerformed;
            _subscribed = true;
        }
        
        private void TryUnsubscribe()
        {
            if (!_subscribed) return;
            if (gameInput == null) return;

            try
            {
                gameInput.Gameplay.Interact.performed -= OnInteractPerformed;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Failed to unsubscribe safely: {ex}");
            }

            _subscribed = false;
        }

        private void Update()
        {
            CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            if (_zone.InRange.Count == 0)
            {
                ResetInteraction();
                return;
            }

            var camTransform = _mainCharacterCamera._camera.transform;
            var ray = new Ray(camTransform.position, camTransform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, _sphereRadius, out hit, _rayDistance))
            {
                if (hit.collider.CompareTag(_interactableTag))
                {
                    if (hit.collider.TryGetComponent(out IInteractable interactable))
                    {
                        if (_zone.InRange.Contains(interactable))
                        {
                            if (_currentInteractable != interactable)
                            {
                                _currentInteractable = interactable;
                                _uiManager.Show(interactable); 
                            }
                            return;
                        }
                    }
                }
            }

            ResetInteraction();
        }

        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            if (_currentInteractable != null)
                _currentInteractable.Interact();
        }

        private void ResetInteraction()
        {
            if (_currentInteractable != null)
            {
                _currentInteractable = null;
                _uiManager.Hide();
            }
        }
    }
}