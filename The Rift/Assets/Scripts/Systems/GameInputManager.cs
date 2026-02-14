using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems
{
    public class GameInputManager : IGameInputManager
    {
        public GameInput GameInput { get; private set; }
        
        public GameInputManager()
        {
            GameInput = new GameInput();
            GameInput.Gameplay.Enable();
            Debug.Log("InputManager initialized");
        }
    }
}