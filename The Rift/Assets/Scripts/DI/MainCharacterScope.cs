using DI;
using Entity;
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
            builder.Register<WeaponManager>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<MainCharacterCamera>();


            builder.RegisterComponentInHierarchy<CharacterController>()
                .AsSelf();
            builder.RegisterComponentInHierarchy<MainCharacter.MainCharacter>()
                .AsSelf();
            builder.RegisterComponentInHierarchy<MainCharacterMovement>()
                .As<IControllable>()      
                .AsSelf();
            builder.RegisterComponentInHierarchy<MainCharacterMovementController>()
                .AsSelf();
            builder.RegisterComponentInHierarchy<MainCharacterAttackController>()
                .AsSelf();

            builder.RegisterComponentInHierarchy<InteractionUIManager>();
            

            


            builder.RegisterEntryPoint<MainCharacterInitializer>();



        }
    }
}