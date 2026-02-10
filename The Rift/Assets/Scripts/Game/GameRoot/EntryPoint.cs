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
        readonly IUIRootView _uiRootPrefab;
        readonly IGameManager _gameManager;
        
        public void Start()
        {
            _gameManager.SetState(GameState.Gameplay);
            RunGame();
        }

        
        private EntryPoint(
            ICoroutineRunner coroutines,
            IGameManager gameManager,
            IUIRootView uiRootPrefab)
        {
            _coroutines = coroutines;
            _gameManager = gameManager;
            _uiRootPrefab = uiRootPrefab;
        }
        
        
        private void RunGame() 
        {
            Debug.Log("\n\n============================\nRunGame EntryPoint \n\n============================\n");
            _coroutines.StartRoutine(LoadAndStartGameplay());
        }
        
        private IEnumerator LoadAndStartGameplay()
        {
        
            _uiRootPrefab.ShowLoadingScreen();
            
            yield return LoadScene(Scenes.GAMEPLAY);
            
            yield return new WaitForSeconds(0.5f);

                                    
            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            
            sceneEntryPoint.Run();
            
           
            _uiRootPrefab.HideLoadingScreen();
        }
        
        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
        
        
    }
}