using Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems
{
    public class GameInputManager : IGameInputManager
    {
        public GameInput GameInput { get; private set; }
        private MapsInput currentMap;
        public GameInputManager()
        {
            GameInput = new GameInput();
            GameInput.Gameplay.Enable();
            currentMap = MapsInput.Gameplay;
            Debug.Log("InputManager initialized");
        }

        public void ToggleMap(MapsInput map)
        {
            DisableMap(currentMap);
            EnableMap(map);
            currentMap = map;
        }
        private void EnableMap(MapsInput map)
        {
            switch (map)
            {
                case MapsInput.Gameplay:
                    GameInput.Gameplay.Enable();
                    break;
                
                case MapsInput.UI:
                    GameInput.UI.Enable();
                    break;
            }
        }

        private void DisableMap(MapsInput map)
        {
            switch (map)
            {
                case MapsInput.Gameplay:
                    GameInput.Gameplay.Disable();
                    break;
                case MapsInput.UI:
                    GameInput.UI.Disable();
                    break;
            }
        }
    }
}