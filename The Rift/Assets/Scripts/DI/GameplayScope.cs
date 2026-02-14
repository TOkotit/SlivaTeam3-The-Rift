using Game.Gameplay.Root;
using VContainer;
using VContainer.Unity;
using Systems;
using MainCharacter;
using UnityEngine;
using Game.Gameplay.View.UI;
using UIRoot;
using Utils;

namespace DI
{
    public class GameplayScope: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameplayUIRootViewModel>(Lifetime.Singleton);
            builder.Register<GameplayUIManager>(Lifetime.Singleton);
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
            builder.RegisterComponentInHierarchy<MainCharacterCamera>();
            builder.Register<CharacterController>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<MainCharacter.MainCharacter>()
                .AsSelf();
            builder.RegisterComponentInHierarchy<CharacterMovement>()
                .As<IControllable>()      
                .AsSelf();
            builder.RegisterComponentInHierarchy<MainCharacterInputController>()
                .AsSelf();
            builder.Register<MainCharacterModel>(Lifetime.Singleton);


            builder.RegisterEntryPoint<GameplayEntryPoint>(Lifetime.Scoped);

        }
    }
}