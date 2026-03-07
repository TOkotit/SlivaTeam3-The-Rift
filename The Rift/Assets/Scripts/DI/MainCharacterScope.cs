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
        [SerializeField] private MovementStatsSO stats;
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("MainCharacterScope.Configure called");
            builder.RegisterInstance(stats); 
            
            builder.Register<WeaponManager>(Lifetime.Singleton);
            
            builder.RegisterComponentInHierarchy<MainCharacter.MainCharacter>();
            
            builder.RegisterComponentInHierarchy<MainCharacterCamera>();

            builder.RegisterComponentInHierarchy<CharacterController>()
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