using Entity.Runes;
using Game.Inventory.Runes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Gameplay.View.UI.ScreenForge
{
    public class WeaponRuneSlotView : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image iconImage; 
        [SerializeField] private Image backgroundImage;
        [SerializeField] private TextMeshProUGUI slotTypeText;
        
        public RuneData ContainedRune { get; private set; }
        public bool IsEmpty => ContainedRune == null;
        
        private GameObject _dragGhost;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        
        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            UpdateVisuals(); 
        }
        
        public void SetSlotType(RuneSlotsType type)
        {
            if (slotTypeText != null)
                slotTypeText.text = type.ToString();
        }
        
        
        public void SetRune(RuneData rune)
        {
            ContainedRune = rune;
            UpdateVisuals();
        }
        
        public void ClearSlot()
        {
            ContainedRune = null;
            UpdateVisuals();
        }
        
        private void UpdateVisuals()
        {
            if (IsEmpty)
            {
                iconImage.gameObject.SetActive(false);
                iconImage.sprite = null;
                if (backgroundImage != null) backgroundImage.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                iconImage.sprite = ContainedRune.icon;
                iconImage.gameObject.SetActive(true);
                if (backgroundImage != null) backgroundImage.color = new Color(1, 1, 1, 1f);
            }
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            var draggedObject = eventData.pointerDrag;
            if (draggedObject == null) return;
            
            
            RuneData incomingRuneData = null;

            if (draggedObject.TryGetComponent<RuneSlotView>(out var sourceListView))
                incomingRuneData = sourceListView.Data;

            else if (draggedObject.TryGetComponent<WeaponRuneSlotView>(out var sourceSlotView))
            {
                incomingRuneData = sourceSlotView.ContainedRune;
                if (sourceSlotView != this)
                    sourceSlotView.ClearSlot();
            }

            if (incomingRuneData != null && IsEmpty)
            {
                SetRune(incomingRuneData);
                Debug.Log($"UI: Rune {incomingRuneData.runeName} dropped into slot.");
            }
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsEmpty || _canvas == null)
            {
                eventData.pointerDrag = null;
                return;
            }

            _dragGhost = new GameObject("DragGhost_FromSlot");
            _dragGhost.transform.SetParent(_canvas.transform, false);
            _dragGhost.transform.SetAsLastSibling();

            var image = _dragGhost.AddComponent<Image>();
            image.sprite = ContainedRune.icon;
            image.raycastTarget = false; 

            var rect = _dragGhost.GetComponent<RectTransform>();
            rect.sizeDelta = GetComponent<RectTransform>().sizeDelta;

            UpdateGhostPosition(eventData.position);

            _canvasGroup.blocksRaycasts = false;
    
            iconImage.gameObject.SetActive(false);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_dragGhost != null) UpdateGhostPosition(eventData.position);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if (_dragGhost != null) Destroy(_dragGhost);

            _canvasGroup.blocksRaycasts = true;
            
            if (!eventData.used && !IsEmpty)
            {
                Debug.Log($"UI: Rune {ContainedRune.runeName} removed from slot (dropped to empty space).");
                ClearSlot();
            }
            else
            {
                UpdateVisuals();
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