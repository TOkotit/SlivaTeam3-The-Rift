using System;
using UnityEngine;
using UnityEngine.Events;

// Старый код. Просто приделал интерфейс. А так в основном то же самое

namespace Systems
{
    public class GameManager : IGameManager
    {
        public GameManager()
        {
            CurrentState = GameState.Booting;
            OnStateChange = new UnityEvent<GameState>();
            Debug.Log("GameManager: Инициализирован.");
        }
        
        public GameState CurrentState { get; private set; }
        
        public UnityEvent<GameState> OnStateChange { get; }
        
        public void SetState(GameState newState)
        {
            if (CurrentState == newState) return;

            CurrentState = newState;
            Debug.Log($"Game State changed to: {newState}");

            OnStateChange?.Invoke(newState);

            Time.timeScale = newState switch
            {
                GameState.Gameplay => 1f,
                GameState.Paused => 0f,
                GameState.GameOver => 0f,
                _ => Time.timeScale
            };
        }
        
    }
}