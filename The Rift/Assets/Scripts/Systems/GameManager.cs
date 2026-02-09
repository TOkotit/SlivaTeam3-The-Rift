using System;
using UnityEngine;
using UnityEngine.Events;

// Код из старого проекта. Будет пересмотрено и переделано

namespace Systems
{
    public class GameManager
    {
        
        private GameManager()
        {
            CurrentState = GameState.Booting;
            Debug.Log("GameManager: Инициализирован.");
        }
        
        public GameState CurrentState { get; private set; }
        
        public UnityEvent<GameState> OnStateChange;
        
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