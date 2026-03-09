using System;
using Entity.Runes;
using Game.Inventory.Runes;
using Game.UI;
using MainCharacter;
using R3;
using Systems;
using UnityEngine;
using Utils;
using VContainer;

namespace Game.Gameplay.View.UI.ScreenForge
{
    public class ScreenForgeViewModel : WindowViewModel
    {
        public RuneType SelectedRune { get; private set; }
        public readonly Subject<RuneType> OnRuneSelectedEvent = new();
        private readonly GameplayUIManager _uiManager;
        private readonly IGameManager _gameManager;
        private readonly ICoroutineRunner _coroutines;
        private readonly IGameInputManager _gameInputManager;
        
        private readonly MainCharacterModel  _mainCharacter;
        public override string Id => "ScreenForge";
        
        public readonly RuneManager RuneManager;
        
        
        public ScreenForgeViewModel(GameplayUIManager uiManager, IObjectResolver container)
        {
            _uiManager = uiManager;
            _gameManager =  container.Resolve<IGameManager>();
            _coroutines = container.Resolve<ICoroutineRunner>();
            _mainCharacter = container.Resolve<MainCharacterModel>();
            _gameInputManager = container.Resolve<IGameInputManager>();
            RuneManager = container.Resolve<RuneManager>();
        }
        
        
        public void RequestSubText(Action<int> f)
        {
            Debug.Log($"RequestSubText {_mainCharacter.Health == null}");
            _mainCharacter.Health.OnHealthChanged += f;
        }
        
        public void RequestUnsubText(Action<int> f)
        {
            Debug.Log($"RequestUnsubText {_mainCharacter.Health == null}");
            _mainCharacter.Health.OnHealthChanged -= f;
        }


        public void RequestGoToScreenGameplay()
        {
            _uiManager.OpenScreenGameplay();
        }
        
        public void OnRuneSelected(RuneType type)
        {
            SelectedRune = type; // Записываем руну
            OnRuneSelectedEvent.OnNext(type); // Уведомляем UI
            Debug.Log($"Rune {type} is now active");
        }
        
    }
}