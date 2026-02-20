using Game.Gameplay;
using Game.Gameplay.Root;
using VContainer;
using VContainer.Unity;
using Systems;
using MainCharacter;
using UnityEngine;
using Game.Gameplay.View.UI;
using Game.Inventory;
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
            
            builder.Register<GameData>(Lifetime.Singleton);
            
            builder.Register<Inventory>(Lifetime.Singleton);
            builder.Register<InventoryManager>(Lifetime.Singleton);


            Debug.Log("GameplayScope.Configure called");
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