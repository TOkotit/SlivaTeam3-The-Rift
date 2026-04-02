using System;
using Entity.Runes;
using MainCharacter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace Game.Gameplay.View.UI.ScreenForge
{
    
    public class RuneSlotView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Button button;
        
        private GameObject _dragGhost;
        private Canvas _canvas;
        public RuneType SlotType { get; private set; }
        public RuneData Data { get; private set; }
        public void Setup(RuneData data)
        {
            Data = data;
            iconImage.sprite = data.icon;
            SlotType = data.Rune;
            _canvas = GetComponentInParent<Canvas>();
        }
        
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragGhost = new GameObject("DragGhost");
            _dragGhost.transform.SetParent(_canvas.transform, false);
            _dragGhost.transform.SetAsLastSibling();

            var image = _dragGhost.AddComponent<Image>();
            image.sprite = iconImage.sprite;
            image.raycastTarget = false; 
            
            var rect = _dragGhost.GetComponent<RectTransform>();
            rect.sizeDelta = GetComponent<RectTransform>().sizeDelta;
            
            UpdateGhostPosition(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragGhost != null)
            {
                UpdateGhostPosition(eventData.position);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_dragGhost != null)
            {
                Destroy(_dragGhost);
            }
        }
        
        private void UpdateGhostPosition(Vector2 screenPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                screenPosition,
                _canvas.worldCamera,
                out var localPoint);
            
            _dragGhost.transform.localPosition = localPoint;
        }
    }
}