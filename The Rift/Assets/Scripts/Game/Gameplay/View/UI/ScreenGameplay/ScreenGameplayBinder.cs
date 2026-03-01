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
        
        [SerializeField] private TextMeshProUGUI _healthText;

        public TextMeshProUGUI HealthText
        {
            get => _healthText;
            set => _healthText = value;
        }

        private void Start()
        {
            _btnGoToMainMenu?.onClick.AddListener(OnGoToMainMenuButtonClicked);
            
            ViewModel.inithealthText(UpdateHealthText);
            ViewModel.RequestSubText(UpdateHealthText);
        }

        private void OnDestroy()
        {
            _btnGoToMainMenu?.onClick.RemoveListener(OnGoToMainMenuButtonClicked);

            
            ViewModel.RequestUnsubText(UpdateHealthText);
        }

        private void UpdateHealthText(int newValue)
        {
            HealthText.text = newValue.ToString();
        }
        
        private void OnGoToMainMenuButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }
        
    }
}