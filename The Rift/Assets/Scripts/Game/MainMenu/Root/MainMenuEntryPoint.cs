using System.Collections;
using Root;
using Systems;
using UIRoot;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Utils;
using VContainer.Unity;

public class MainMenuEntryPoint : IStartable
{
    readonly IGameManager _gameManager;
    
    public void Start()
    {
        Debug.Log("Меню: Контейнер запущен, логика инициализирована.");
        
        _gameManager.SetState(GameState.Menu);
    }

    public MainMenuEntryPoint(IGameManager gameManager)
    {
        _gameManager = gameManager;
        
    }
}
