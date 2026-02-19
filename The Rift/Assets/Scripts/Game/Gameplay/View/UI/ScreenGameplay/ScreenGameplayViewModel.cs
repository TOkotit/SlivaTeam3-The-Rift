using Game.UI;
using R3;
using Systems;
using Utils;
using VContainer;

namespace Game.Gameplay.View.UI
{
    public class ScreenGameplayViewModel : WindowViewModel
    {
        private readonly GameplayUIManager _uiManager;
        private readonly IGameManager _gameManager;
        private readonly ICoroutineRunner _coroutines;
        public override string Id => "ScreenGameplay";

        public ScreenGameplayViewModel(GameplayUIManager uiManager, IObjectResolver container)
        {
            _uiManager = uiManager;
            _gameManager =  container.Resolve<IGameManager>();
            _coroutines = container.Resolve<ICoroutineRunner>();
        }

        public void RequestOpenPopupA()
        {
            _uiManager.OpenPopupA();
        }
        
        public void RequestGoToMainMenu()
        {
            _coroutines.StartRoutine(_gameManager.LoadMainMenu());
        }
    }
}