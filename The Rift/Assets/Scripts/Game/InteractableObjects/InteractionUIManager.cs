using MainCharacter;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game
{
    public class InteractionUIManager : MonoBehaviour
    {
        [SerializeField] private Canvas _targetCanvas;
        [SerializeField] private GameObject _promptPrefab;
        [SerializeField] private Vector3 _offset = new Vector3(0, 1.5f, 0);
        
        private GameObject _promptInstance;
        private Camera _mainCam;
        private Transform _currentTarget;
        private bool _isInitialized;

        [Inject]
        public void Construct(MainCharacterCamera mainCamera)
        {
            if (_isInitialized || _promptInstance != null) return;
            
            
            _mainCam = mainCamera._camera;

            if (_promptPrefab && _targetCanvas)
            {
                _promptInstance = Instantiate(_promptPrefab, _targetCanvas.transform, false);
        
                var rt = _promptInstance.GetComponent<RectTransform>();
                
                rt.localScale = new Vector3(0.01f, 0.01f, 0.01f); 
        
                rt.localPosition = Vector3.zero;
                rt.localRotation = Quaternion.identity;

                _promptInstance.SetActive(false);
                _isInitialized = true;
            }
        }

        /// <summary>
        /// Вызывается контроллером, когда игрок смотрит на объект
        /// </summary>
        public void Show(IInteractable target)
        {
            if (!_isInitialized || target == null) return;
            _currentTarget = target.InteractionPoint;
            _promptInstance.SetActive(true);
            UpdatePosition();
           
        }
        /// <summary>
        /// Вызывается контроллером, когда игрок отворачивается
        /// </summary>
        public void Hide()
        {
            if (!_isInitialized) return;
            _promptInstance.SetActive(false);
            _currentTarget = null;
        }
        
        private void LateUpdate()
        {
            if (_isInitialized && _promptInstance.activeSelf)
            {
                UpdatePosition();
            }
        }
        
        private void UpdatePosition()
        {
            if (_currentTarget == null || _mainCam == null) return;

            _promptInstance.transform.position = _currentTarget.position + _offset;

            var lookDir = _promptInstance.transform.position - _mainCam.transform.position;
            if (lookDir != Vector3.zero)
            {
                _promptInstance.transform.rotation = Quaternion.LookRotation(lookDir);
            }
        }
        
    }
}