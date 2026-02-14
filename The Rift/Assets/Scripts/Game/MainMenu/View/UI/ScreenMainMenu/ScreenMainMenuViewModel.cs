using Game.Gameplay.View.UI;
using Game.UI;
using R3;

namespace Game.MainMenu.View.UI.ScreenMainMenu
{
    public class ScreenMainMenuViewModel : WindowViewModel
    {
        public override string Id =>  "ScreenMainMenu";
        
        private readonly MainMenuUIManager _uiManager;
        private readonly Subject<Unit> _exitSceneRequest;

        public ScreenMainMenuViewModel(MainMenuUIManager uiManager, Subject<Unit> exitSceneRequest)
        {
            _uiManager = uiManager;
            _exitSceneRequest = exitSceneRequest;
        }

        public void RequestPlay()
        {
            
        }
        
        public void RequestContinue()
        {
            
        }
        
        
    }
}