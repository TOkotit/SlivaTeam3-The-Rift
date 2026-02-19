using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Systems
{
    public interface IGameInputManager
    {
        GameInput GameInput { get; }
    }
}