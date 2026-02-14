using Game.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainMenu.View.UI.ScreenMainMenu
{
    public class ScreenMainMenuBinder : WindowBinder<ScreenMainMenuViewModel>
    {
        [SerializeField] private Button _btnPlay;
        [SerializeField] private Button _btnContinue;
        [SerializeField] private Button _btnOptions;
        [SerializeField] private Button _btnExit;


        private void OnEnable()
        {
            _btnPlay?.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDisable()
        {

        }

        private void OnPlayButtonClicked()
        {
            ViewModel.RequestPlay();
        }
        
        private void OnContinueButtonClicked()
        {
            
        }
        
        private void OnOptionsButtonClicked()
        {
            
        }
        
        private void OnExitButtonClicked()
        {
            
        }
        
    }
}