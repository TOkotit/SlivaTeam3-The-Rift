using Game.UI;
using MainCharacter;
using Systems;
using Utils;
using VContainer;

namespace Game.Gameplay.View.UI.ScreenPauseMenu
{
    public class ScreenPauseMenuViewModel : WindowViewModel
    {
        private readonly GameplayUIManager _uiManager;
        private readonly IGameManager _gameManager;
        private readonly ICoroutineRunner _coroutines;
        private readonly IGameInputManager _gameInputManager;
        
        
        public override string Id => "ScreenPauseMenu";
        
        public ScreenPauseMenuViewModel(GameplayUIManager uiManager, IObjectResolver container)
        {
            _uiManager = uiManager;
            _gameManager =  container.Resolve<IGameManager>();
            _coroutines = container.Resolve<ICoroutineRunner>();
            
            _gameInputManager = container.Resolve<IGameInputManager>();
            
        }

        public void RequestGoToScreenGameplay()
        {
            _uiManager.OpenScreenGameplay();
        }
        
        public void RequestGoToMainMenu()
        {
            _coroutines.StartRoutine(_gameManager.LoadMainMenu());
        }
        
        public void RequestGoToScreenOptions()
        {
            // _uiManager.OpenScreenGameplay();
        }
    }
}