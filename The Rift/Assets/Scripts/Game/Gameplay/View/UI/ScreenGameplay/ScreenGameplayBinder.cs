using System;
using Game.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.View.UI
{
    public class ScreenGameplayBinder : WindowBinder<ScreenGameplayViewModel>
    {
        [SerializeField] private Button _btnGoToMainMenu;
        [SerializeField] private Button _btnPopupA;
        [SerializeField] private TextMeshProUGUI _healthText;

        private void OnEnable()
        {
            _btnGoToMainMenu?.onClick.AddListener(OnGoToMainMenuButtonClicked);
            _btnPopupA?.onClick.AddListener(OnPopupAButtonClicked);
            
            ViewModel.inithealthText(UpdateHealthText);
            ViewModel.RequestSubText(UpdateHealthText);
            
        }

        private void OnDisable()
        {
            _btnGoToMainMenu?.onClick.RemoveListener(OnGoToMainMenuButtonClicked);
            _btnPopupA?.onClick.RemoveListener(OnPopupAButtonClicked);
            
            ViewModel.RequestUnsubText(UpdateHealthText);
        }

        private void UpdateHealthText(int newValue)
        {
            _healthText.text = newValue.ToString();
        }
        
        private void OnGoToMainMenuButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }
        
        private void OnPopupAButtonClicked()
        {
            ViewModel.RequestOpenPopupA();
        }
    }
}