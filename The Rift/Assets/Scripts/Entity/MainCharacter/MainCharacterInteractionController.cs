using Game;
using UnityEngine;
using VContainer;

namespace MainCharacter
{
    public class MainCharacterInteractionController : MonoBehaviour
    {
        [Inject] private MainCharacterCamera _mainCharacterCamera;
        [Inject] private InteractionUIManager _uiManager;
        
        
        [Header("Ссылки")]
        [SerializeField] private InteractionZone _zone; 

        [Header("Настройки")]
        [SerializeField] private float _sphereRadius = 0.3f;
        [SerializeField] private float _rayDistance = 4f; 
        [SerializeField] private string _interactableTag = "Interactable";

        private IInteractable _currentInteractable;
        
        
        private void Update()
        {
            CheckForInteractable();
            HandleInput();
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

        private void HandleInput()
        {
            if (_currentInteractable != null && Input.GetKeyDown(KeyCode.E))
            {
                _currentInteractable.Interact();
            }
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