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
          
            Debug.Log("GameplayScope.Configure called");

            builder.Register<GameplayUIRootViewModel>(Lifetime.Singleton);
            builder.Register<GameplayUIManager>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<MainCharacterCamera>();
            builder.Register<CharacterController>(Lifetime.Scoped);
            builder.Register<GameData>(Lifetime.Singleton);

            builder.Register<IGameInputManager, GameInputManager>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<MainCharacter.MainCharacter>()
                .AsSelf();
            builder.RegisterComponentInHierarchy<CharacterMovement>()
                .As<IControllable>()      
                .AsSelf();
            builder.RegisterComponentInHierarchy<MainCharacterInputController>()
                .AsSelf();

            builder.Register<MainCharacterModel>(Lifetime.Singleton)
                .WithParameter(typeof(Health), new Health(200));


            builder.Register<Inventory>(Lifetime.Singleton);
            builder.Register<InventoryManager>(Lifetime.Singleton); 

            

            builder.RegisterEntryPoint<GameplayEntryPoint>(Lifetime.Scoped);

        }
    }
}