using DI;
using Game;
using Game.Gameplay.View.UI;
using Game.MainMenu.View.UI;
using MainCharacter;
using R3;
using Systems;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace DI
{
    /// <summary>
    /// Контейнер для штук персонажа
    /// </summary>
    public class MainCharacterScope : LifetimeScope
    {
        
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("MainCharacterScope.Configure called");
            
            builder.RegisterComponentInHierarchy<MainCharacterCamera>();
            builder.Register<CharacterController>(Lifetime.Singleton);
            
            builder.RegisterComponentInHierarchy<MainCharacter.MainCharacter>()
                .AsSelf();
            builder.RegisterComponentInHierarchy<CharacterMovement>()
                .As<IControllable>()      
                .AsSelf();
            builder.RegisterComponentInHierarchy<MainCharacterMovementController>()
                .AsSelf();
            
            builder.RegisterComponentInHierarchy<InteractionUIManager>();
            
            builder.RegisterComponentInHierarchy<MainCharacterAttackController>();


            builder.RegisterEntryPoint<MainCharacterInitializer>();



        }
    }
}