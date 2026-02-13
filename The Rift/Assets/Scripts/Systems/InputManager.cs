using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems
{
    public class InputManager : IInputManager
    {
        static InputManager _instance;
        private GameInput _gameInput;
        public  GameInput GameInput=> _gameInput;

        public InputManager()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Gameplay.Enable();
            }
        }
        
    }
}