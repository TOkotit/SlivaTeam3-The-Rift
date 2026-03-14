using System.Runtime.InteropServices;
using Enums;
using Game.Gameplay.View.UI.ScreenForge;
using Game.UI;
using MainCharacter;
using R3;
using Systems;
using VContainer;
using UnityEngine;

namespace Game.Gameplay.View.UI
{
    public class GameplayUIManager : UIManager
    {
        private MainCharacterCamera _mainCharacterCamera;
        private MainCharacterMovement _mainCharacterMovement;
        private IGameInputManager _gameInputManager;
        private MainCharacterAttackController _attackController;
        public GameplayUIManager(IObjectResolver container) : base(container)
        {
            _mainCharacterCamera = Container.Resolve<MainCharacterCamera>();
            _mainCharacterMovement = Container.Resolve<MainCharacterMovement>();
            _gameInputManager = Container.Resolve<IGameInputManager>();
            _attackController = Container.Resolve<MainCharacterAttackController>();
        }
        
        

        public ScreenGameplayViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel(this, Container);
            var rootUI = Container.Resolve<GameplayUIRootViewModel>();
            _mainCharacterMovement.UnlockMovement();
            LockUpCursor();
            UnlockCamera();
            rootUI.OpenScreen(viewModel);
            
            _gameInputManager.ToggleMap(MapsInput.Gameplay);
            
            return viewModel;
        }
        
        public ScreenForgeViewModel OpenScreenForge()
        {
            var viewModel = new ScreenForgeViewModel(this, Container);
            var rootUI = Container.Resolve<GameplayUIRootViewModel>();
            
            
            _mainCharacterMovement.LockUpMovement();
            UnlockCursor();
            LockUpCamera();
            rootUI.OpenScreen(viewModel);
            _gameInputManager.ToggleMap(MapsInput.UI);

            return viewModel;
        }
        
        
        // блокировка камеры
        public void LockUpCamera()
        {
            _mainCharacterCamera.IsCameraRotating = false;
        }
        
        
        public void UnlockCamera()
        {
            _mainCharacterCamera.IsCameraRotating = true;
        }
        // Блокировать или разблокировать курсор
        
        public void LockUpCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}