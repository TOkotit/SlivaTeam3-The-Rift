using System.Runtime.InteropServices;
using Enums;
using Game.Gameplay.View.UI.ScreenForge;
using Game.Gameplay.View.UI.ScreenInventory;
using Game.Gameplay.View.UI.ScreenPauseMenu;
using Game.UI;
using MainCharacter;
using R3;
using Systems;
using VContainer;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.View.UI
{
    public class GameplayUIManager : UIManager
    {
        private MainCharacterCamera _mainCharacterCamera;
        private MainCharacterMovement _mainCharacterMovement;
        private IGameInputManager _gameInputManager;
        private MainCharacterAttackController _attackController;

        private GameplayUIRootViewModel rootUI;
        
        public GameplayUIManager(IObjectResolver container) : base(container)
        {
            rootUI = Container.Resolve<GameplayUIRootViewModel>();
            
            _mainCharacterCamera = Container.Resolve<MainCharacterCamera>();
            _mainCharacterMovement = Container.Resolve<MainCharacterMovement>();
            _gameInputManager = Container.Resolve<IGameInputManager>();
            _attackController = Container.Resolve<MainCharacterAttackController>();
            

            _gameInputManager.GameInput.Gameplay.Inventory.performed += OnToggleInventory;
            _gameInputManager.GameInput.Gameplay.PauseMenu.performed += OnTogglePause;
        }
        
        

        public ScreenGameplayViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel(this, Container);
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
            
            
            _mainCharacterMovement.LockUpMovement();
            UnlockCursor();
            LockUpCamera();
            rootUI.OpenScreen(viewModel);
            _gameInputManager.ToggleMap(MapsInput.UI);

            return viewModel;
        }
        private void OnTogglePause(InputAction.CallbackContext c)
        {
            if (rootUI.OpenedScreen.CurrentValue is not ScreenPauseMenuViewModel)
            {
                OpenScreenPauseMenu();
            }
            else
            {
                OpenScreenGameplay();
            }
            
        }

        private void OnToggleInventory(InputAction.CallbackContext c)
        {
            if (rootUI.OpenedScreen.CurrentValue is not ScreenInventoryViewModel)
            {
                OpenScreenInventory();
            }
            else
            {
                OpenScreenGameplay();
            }
            
        }

        public ScreenInventoryViewModel OpenScreenInventory()
        {
            var viewModel = new ScreenInventoryViewModel(this, Container);
            
            
            _mainCharacterMovement.LockUpMovement();
            UnlockCursor();
            LockUpCamera();
            rootUI.OpenScreen(viewModel);
            _gameInputManager.ToggleMap(MapsInput.UI);

            return viewModel;
        }
        
        public ScreenPauseMenuViewModel OpenScreenPauseMenu()
        {
            var viewModel = new ScreenPauseMenuViewModel(this, Container);
            
            
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