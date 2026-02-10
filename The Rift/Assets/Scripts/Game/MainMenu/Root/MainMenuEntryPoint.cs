using System.Collections;
using Root;
using Systems;
using UIRoot;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Utils;
using VContainer.Unity;

public class MainMenuEntryPoint : MonoBehaviour
{
    
    private readonly ICoroutineRunner _coroutines;
    readonly IUIRootView _uiRoot;
    readonly IGameManager _gameManager;
    
    public void Start()
    {
    }

    public MainMenuEntryPoint(            
        ICoroutineRunner coroutines,
        IGameManager gameManager,
        IUIRootView uiRootPrefab)
    {
        _coroutines = coroutines;
        _gameManager = gameManager;
        _uiRoot = uiRootPrefab;
        
    }

    public void StartGameplay()
    {
        _gameManager.SetState(GameState.Gameplay);
            
        _coroutines.StartRoutine(InitialLoadRoutine());
    }
    
    private IEnumerator InitialLoadRoutine()
    {
        _uiRoot.ShowLoadingScreen();

        yield return new WaitForSeconds(0.2f); 
            
        yield return SceneManager.LoadSceneAsync(Scenes.GAMEPLAY);

        _gameManager.SetState(GameState.Gameplay);

        _uiRoot.HideLoadingScreen();
    }
}
