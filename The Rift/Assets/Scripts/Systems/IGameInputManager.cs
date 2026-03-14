using Enums;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Systems
{
    public interface IGameInputManager
    {
        GameInput GameInput { get; }
        public void ToggleMap(MapsInput map);
    }
}