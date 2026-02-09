using System;
using UnityEngine.Events;

namespace Systems
{
    /// <summary>
    /// Интерфейс для управления состоянием игры
    ///
    /// Отвечает за текущее состояние игры (меню, геймплей, пауза и т.д.)
    /// и уведомляет подписчиков об изменениях состояния.
    /// Является синглтоном и берёться через DI
    /// </summary>
    public interface IGameManager
    {
        /// <summary>
        /// Текущее состояние игры.
        /// </summary>
        GameState CurrentState { get; }
        
        /// <summary>
        /// Событие, вызываемое при изменении состояния игры.
        /// </summary>
        UnityEvent<GameState> OnStateChange { get; }
        
        /// <summary>
        /// Устанавливает новое состояние игры.
        /// Если состояние не изменилось — событие не вызывается.
        /// </summary>
        void SetState(GameState newState);
    }
}