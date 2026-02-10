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
        private readonly IObjectResolver _resolver;
        
        
        //----------------------------------------------------------------------------------------
        // Из гайда лавки разработчика. Штука отвечает за весь UI и я не уверен что мы это оставим
        // Нужно было передавать как префаб от сцены к сцене
        // По большей части нужно было только ради экрана загрузки между сценами
        
        // По идее должно работать, но я не тестил
        //----------------------------------------------------------------------------------------
        readonly IUIRootView _uiRootPrefab;
        readonly IGameManager _gameManager;
        
        public void Start()
        {
            _gameManager.SetState(GameState.Gameplay);
            RunGame();
        }

        
        // вот как-то не очень что сюда столько всего пихаем, надо подумать можно ли получше сделать
        private EntryPoint(IObjectResolver resolver,
            ICoroutineRunner coroutines,
            IGameManager gameManager,
            IUIRootView uiRootPrefab)
        {
            _resolver = resolver;
            _coroutines = coroutines;
            _resolver.Inject(_coroutines);
            _gameManager = gameManager;
            _resolver.Inject(_gameManager);
            _uiRootPrefab = uiRootPrefab;
            _resolver.Inject(_uiRootPrefab);
        }
        
        
        private void RunGame() 
        {
            Debug.Log("\n============================ RunGame EntryPoint ============================\n");
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
    
    
    
    
    //----------------------------------------------------------------
    // блок кода с прошлого проекта. Буду под DI это переделывать
    // Большая часть это мусор который я удалю, но часть вроде нужное
    // Какую-то часть я уже перенёс, вроде норм
    //----------------------------------------------------------------
    
    // public class GameEntryPoint
    // {
    //     
    //     [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //     public static void AfterStart()
    //     {
    //         
    //         var currentSceneName = SceneManager.GetActiveScene().name;
    //         
    //         if (_instance == null)
    //         {
    //             _instance = new GameEntryPoint();
    //         }
    //         
    //         _instance.RunGame();
    //     }
    //
    //
    //
    //     private GameEntryPoint()
    //     {
    //         _coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
    //         Object.DontDestroyOnLoad(_coroutines.gameObject);
    //
    //         var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
    //         _uiRoot = Object.Instantiate(prefabUIRoot);
    //         Object.DontDestroyOnLoad(_uiRoot.gameObject);
    //
    //         if (Game.Instance == null)
    //             Game.Initialize();
    //     }
    //
    //     private void RunGame() 
    //     {
    //         #if UNITY_EDITOR
    //             var sceneName = SceneManager.GetActiveScene().name;
    //             if (sceneName == Scenes.GAMEPLAY)
    //             {
    //                 _coroutines.StartCoroutine(LoadAndStartGameplay());
    //
    //                 return;
    //             }
    //
    //             if (sceneName == Scenes.MAIN_MENU)
    //             {
    //                 _coroutines.StartCoroutine(LoadAndStartMainMenu());
    //
    //                 return;
    //             }
    //
    //             if (sceneName == Scenes.LEVEL_SELECT)
    //             {
    //                 _coroutines.StartCoroutine(LoadAndStartLevelSelect());
    //
    //                 return;
    //             }
    //
    //             if (sceneName != Scenes.BOOT)
    //             {
    //                 return;
    //             }
    //         #endif
    //
    //         _coroutines.StartCoroutine(LoadAndStartMainMenu());
    //     }
    //    
    //     
    //     private void OnLevelCompletedHandle(string levelId)
    //     {
    //         _coroutines.StartCoroutine(DelayedReturnToLevelSelect());
    //     }
    //     
    //     private IEnumerator DelayedReturnToLevelSelect()
    //     {
    //         Debug.Log("Уровень завершен. Ждем 5 секунд...");
    //         yield return new WaitForSeconds(5f);
    //         _coroutines.StartCoroutine(LoadAndStartLevelSelect());
    //         
    //     }
    //
    //     
    //
    //     private IEnumerator LoadAndStartLevelSelect()
    //     {
    //         _uiRoot.ShowLoadingScreen();
    //
    //         yield return LoadScene(Scenes.BOOT);
    //         yield return LoadScene(Scenes.LEVEL_SELECT);
    //
    //         yield return new WaitForEndOfFrame();
    //         yield return new WaitForSeconds(0.5f);
    //
    //         var sceneEntryPoint = LevelSelectorEntryPoint.Instance;
    //
    //         if (sceneEntryPoint)
    //         {
    //             sceneEntryPoint.Run(_uiRoot);
    //         }
    //         else
    //         {
    //             Debug.LogError("Ошибка: Не найдена точка входа в сцену LevelSelect!");
    //             yield break;
    //         }
    //
    //         sceneEntryPoint.GoToMainMenuSceneRequested += () =>
    //         {
    //             _coroutines.StartCoroutine(LoadAndStartMainMenu());
    //         };
    //         
    //         sceneEntryPoint.GoToGameplaySceneRequested += (levelId) =>
    //         {
    //             _currentLevelId = levelId;
    //             Game.Instance.CurrentLevelId = levelId;
    //             _coroutines.StartCoroutine(LoadAndStartGameplay());
    //         };
    //
    //         sceneEntryPoint.GoToBuffsMenuSceneRequested += () =>
    //         {
    //             _coroutines.StartCoroutine(LoadAndStartBuffsMenu());
    //         };
    //
    //         GameManager.Instance.SetState(GameState.LevelSelect);
    //         _uiRoot.HideLoadingScreen();
    //     }
    //
    //     private IEnumerator LoadAndStartBuffsMenu()
    //     {
    //         _uiRoot.ShowLoadingScreen();
    //
    //         yield return LoadScene(Scenes.BOOT);
    //         yield return LoadScene(Scenes.BUFFS_MENU);
    //         
    //         yield return new WaitForSeconds(0.5f);
    //
    //         var sceneEntryPoint =  BuffsMenuEntryPoint.Instance;
    //
    //         if (sceneEntryPoint)
    //         {
    //             sceneEntryPoint.Run(_uiRoot);
    //         }
    //         else
    //         {
    //             Debug.LogError("Ошибка: Не найдена точка входа в сцену LevelSelect!");
    //         }
    //         
    //         sceneEntryPoint.GoToLevelSelectSceneRequested += () =>
    //         {
    //             _coroutines.StartCoroutine(LoadAndStartLevelSelect());
    //         };
    //         
    //         GameManager.Instance.SetState(GameState.LevelSelect);
    //         _uiRoot.HideLoadingScreen();
    //     }
    //     

    // }
}