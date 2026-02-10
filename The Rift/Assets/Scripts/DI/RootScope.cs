using Systems;
using UIRoot;
using Unity.VisualScripting;
using UnityEngine;
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
            
            var uiRoot = Instantiate(Resources.Load<GameObject>("UIRoot"));
            DontDestroyOnLoad(uiRoot.gameObject);
            var uiRootView = uiRoot.GetComponent<UIRootView>();
            builder.RegisterInstance<IUIRootView>(uiRootView);
            
            builder.Register<IGameManager, GameManager>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<Root.EntryPoint>(Lifetime.Singleton);
        }
    }
}