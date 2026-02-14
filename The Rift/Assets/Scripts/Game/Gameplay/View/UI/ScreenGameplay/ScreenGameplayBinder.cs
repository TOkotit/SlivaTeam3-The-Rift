using System;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.View.UI
{
    public class ScreenGameplayBinder : WindowBinder<ScreenGameplayViewModel>
    {
        [SerializeField] private Button _btnGoToMainMenu;
        [SerializeField] private Button _btnPopupA;


        private void OnEnable()
        {
            _btnGoToMainMenu?.onClick.AddListener(OnGoToMainMenuButtonClicked);
            _btnPopupA?.onClick.AddListener(OnPopupAButtonClicked);
        }

        private void OnDisable()
        {
            _btnGoToMainMenu?.onClick.RemoveListener(OnGoToMainMenuButtonClicked);
            _btnPopupA?.onClick.RemoveListener(OnPopupAButtonClicked);
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