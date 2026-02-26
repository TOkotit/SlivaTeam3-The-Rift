using System;
using Game.UI;
using MainCharacter;
using R3;
using Systems;
using UnityEngine;
using Utils;
using VContainer;

namespace Game.Gameplay.View.UI
{
    public class ScreenGameplayViewModel : WindowViewModel
    {
        private readonly GameplayUIManager _uiManager;
        private readonly IGameManager _gameManager;
        private readonly ICoroutineRunner _coroutines;
        private readonly IGameInputManager _gameInputManager;
        
        private readonly MainCharacterModel  _mainCharacter;
        
        public override string Id => "ScreenGameplay";

        public ScreenGameplayViewModel(GameplayUIManager uiManager, IObjectResolver container)
        {
            _uiManager = uiManager;
            _gameManager =  container.Resolve<IGameManager>();
            _coroutines = container.Resolve<ICoroutineRunner>();
            _mainCharacter = container.Resolve<MainCharacterModel>();
            _gameInputManager = container.Resolve<IGameInputManager>();
        }

        // public void inithealthText(Action<int> f)
        // {
        //     Debug.Log("inithealthText");
        //     f(_mainCharacter.Health.CurrentHealth);
        // }
        //
        // public void RequestSubText(Action<int> f)
        // {
        //     Debug.Log("RequestSubText");
        //     _mainCharacter.Health.OnHealthChanged += f;
        // }
        //
        // public void RequestUnsubText(Action<int> f)
        // {
        //     _mainCharacter.Health.OnHealthChanged -= f;
        // }
        
        
        public void RequestGoToMainMenu()
        {
            _coroutines.StartRoutine(_gameManager.LoadMainMenu());
        }
    }
}