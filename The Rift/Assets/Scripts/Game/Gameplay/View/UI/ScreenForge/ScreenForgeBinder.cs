using System;
using Game.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.View.UI.ScreenForge
{
    public class ScreenForgeBinder : WindowBinder<ScreenForgeViewModel>
    {
        [SerializeField] public Button _btnCloseForgeScreen;
        [SerializeField] public Button _btnCreatePage;
        [SerializeField] public Button _btnUpgradePage;
        [SerializeField] public Button _btnGain;

        private void Start()
        {
            _btnCloseForgeScreen?.onClick.AddListener(CloseForgeScreenButtonClicked);
        }
        
        private void OnDestroy()
        {
            _btnCloseForgeScreen?.onClick.RemoveListener(CloseForgeScreenButtonClicked);
        }
        
        private void CloseForgeScreenButtonClicked()
        {
            ViewModel.RequestGoToScreenGameplay();
        }
    }
}