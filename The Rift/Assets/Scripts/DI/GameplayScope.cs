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
            protected override void Configure(IContainerBuilder builder)
            {
              
                builder.Register<GameplayUIRootViewModel>(Lifetime.Singleton);
                builder.Register<GameplayUIManager>(Lifetime.Singleton);
                
                builder.RegisterComponentInHierarchy<MainCharacterCamera>();
                builder.Register<CharacterController>(Lifetime.Scoped);
    
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
                
                builder.RegisterEntryPoint<GameplayEntryPoint>(Lifetime.Scoped);

            
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