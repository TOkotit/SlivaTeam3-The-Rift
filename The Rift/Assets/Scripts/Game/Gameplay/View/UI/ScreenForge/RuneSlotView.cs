using System;
using Entity.Runes;
using MainCharacter;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Gameplay.View.UI.ScreenForge
{
    
    public class RuneSlotView : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Button button;
        [SerializeField] private GameObject outline;
        public RuneType SlotType { get; private set; }
        
        public void Setup(RuneData data, Action onClick)
        {
            iconImage.sprite = data.icon;
            button.onClick.RemoveAllListeners();
            SlotType = data.Rune;
            button.onClick.AddListener(() => onClick?.Invoke());
            SetSelected(false);
        }
        public void SetSelected(bool isSelected)
        {
            if (outline != null) outline.SetActive(isSelected);
        }        
    }
}