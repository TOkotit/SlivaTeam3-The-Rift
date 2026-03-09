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
    
        public void Setup(RuneData data, Action onClick)
        {
            iconImage.sprite = data.icon;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick?.Invoke());
        }
        
    }
}