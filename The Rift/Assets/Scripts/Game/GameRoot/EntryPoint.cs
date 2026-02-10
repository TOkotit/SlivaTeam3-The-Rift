using System.Collections;
using Game.Gameplay.Root;
using Systems;
using UIRoot;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using VContainer;
using VContainer.Unity;

namespace Root
{
    public class EntryPoint : IStartable
    {
        private readonly ICoroutineRunner _coroutines;
        readonly IUIRootView _uiRoot;
        readonly IGameManager _gameManager;
        
        public void Start()
        {
            _gameManager.SetState(GameState.Booting);
            
            _coroutines.StartRoutine(InitialLoadRoutine());
            
        }
        
        private EntryPoint(
            ICoroutineRunner coroutines,
            IGameManager gameManager,
            IUIRootView uiRootPrefab)
        {
            _coroutines = coroutines;
            _gameManager = gameManager;
            _uiRoot = uiRootPrefab;
        }
        
        
        private IEnumerator InitialLoadRoutine()
        {
            _uiRoot.ShowLoadingScreen();

            yield return new WaitForSeconds(0.2f); 
            
            yield return SceneManager.LoadSceneAsync(Scenes.MAINMENU);

            _gameManager.SetState(GameState.Menu);

            _uiRoot.HideLoadingScreen();
        }
        
        
    }
}