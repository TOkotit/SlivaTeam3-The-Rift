using System;
using UnityEngine.Events;

namespace Systems
{
    public interface IGameManager
    {
        GameState CurrentState { get; }
        UnityEvent<GameState> OnStateChange { get; }
        void SetState(GameState newState);
    }
}