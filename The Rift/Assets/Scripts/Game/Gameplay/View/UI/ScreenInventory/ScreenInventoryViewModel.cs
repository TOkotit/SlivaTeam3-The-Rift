using Game.Inventory.Runes;
using Game.UI;
using MainCharacter;
using Systems;
using UnityEngine;
using Utils;
using VContainer;

namespace Game.Gameplay.View.UI.ScreenInventory
{
    public class ScreenInventoryViewModel : WindowViewModel
    {
        private readonly GameplayUIManager _uiManager;
        private readonly IGameManager _gameManager;
        private readonly ICoroutineRunner _coroutines;
        private readonly IGameInputManager _gameInputManager;
        
        public readonly MainCharacterModel  _mainCharacter;

        public override string Id => "ScreenInventory";
        
        public ScreenInventoryViewModel(GameplayUIManager uiManager, IObjectResolver container)
        {
            _uiManager = uiManager;
            _gameManager =  container.Resolve<IGameManager>();
            _coroutines = container.Resolve<ICoroutineRunner>();
            _mainCharacter = container.Resolve<MainCharacterModel>();
            _gameInputManager = container.Resolve<IGameInputManager>();
            
        }

        public void RequestGoToScreenGameplay()
        {
            _uiManager.OpenScreenGameplay();
        }
    }
}