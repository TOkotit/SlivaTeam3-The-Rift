using Systems;
using UIRoot;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class RootScope : LifetimeScope
    {
        
        public static RootScope Instance { get; private set; }
        protected override void Awake()
        {
            if (Instance is not null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
            
            base.Awake();
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            
            
            var coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            DontDestroyOnLoad(coroutines.gameObject);
            builder.RegisterInstance<ICoroutineRunner>(coroutines);
            
            var uiRoot = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Root/UIRoot"));
            DontDestroyOnLoad(uiRoot.gameObject);
            var uiRootView = uiRoot.GetComponent<UIRootView>();
            builder.RegisterInstance<UIRootView>(uiRootView);
            builder.Register<IGameInputManager, GameInputManager>(Lifetime.Singleton);

            Debug.Log("GameplayScope.Configure called");
            builder.Register<IGameManager, GameManager>(Lifetime.Singleton);
            
            
            
            var activeSceneName = SceneManager.GetActiveScene().name;
            
            // Короче, если вам нужен какой-то контейнер на сцене, то вот этот базовый RootScope должен быть либо инициализирован на прошлой сцене
            // Либо он должен быть на вашей сцене. И вы должны указать что он наследуется от RootScope чтобы все данные отсюда тоже перешли в ваш контейнер
            // в игре это всё при запуске создаётся и работает нормально, не надо самому всё это запихивать, но работая на одной сцене вам придётся ручками добавить контейнеры
            // Дальше я написал код который игнорит запуск энтри поинт для того, чтобы при запуске ваша сцена на меню не переключилась.
            // если вы не в boot, то всё инициализируется, но в меню вы не перейдёте
            
            if (Application.isEditor && !string.Equals(activeSceneName, "Boot", System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"Skipping RegisterEntryPoint — running in editor and active scene is '{activeSceneName}' (not 'Boot').");
            }
            else
            {
                builder.RegisterEntryPoint<Root.EntryPoint>(Lifetime.Singleton);
            }
        }
    }
}