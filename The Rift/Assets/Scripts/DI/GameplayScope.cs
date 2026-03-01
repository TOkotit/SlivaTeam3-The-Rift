using Entity;
using Game.Gameplay;
using Game.Gameplay.Root;
using VContainer;
using VContainer.Unity;
using Systems;
using MainCharacter;
using UnityEngine;
using Game.Gameplay.View.UI;
using R3;
using Unity.VisualScripting;
using Utils;
using Game.Inventory;
using UIRoot;

namespace DI
{
    public class GameplayScope: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("GameplayScope.Configure called");
        
            builder.Register<DamagableRegistry>(Lifetime.Singleton);
            builder.Register<AttackSystem>(Lifetime.Singleton);
            
            builder.Register<DamagableRegistry>(Lifetime.Singleton);
            builder.Register<AttackSystem>(Lifetime.Singleton);
            builder.Register<WeaponManager>(Lifetime.Singleton);
            
            
            
            builder.RegisterComponentInHierarchy<MainCharacterAttackController>();
            
            
            builder.RegisterComponentInHierarchy<MainCharacterCamera>();
            builder.Register<CharacterController>(Lifetime.Scoped);
            
            builder.Register<GameData>(Lifetime.Singleton);

            builder.Register<IGameInputManager, GameInputManager>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<MainCharacter.MainCharacter>()
                .AsSelf();
            builder.RegisterComponentInHierarchy<MainCharacterMovement>()
                .As<IControllable>()      
                .AsSelf();
            builder.RegisterComponentInHierarchy<MainCharacterMovementController>()
                .AsSelf();

            builder.Register<Health>(Lifetime.Scoped);
            builder.Register<Stamina>(Lifetime.Scoped);
                
            builder.Register<Inventory>(Lifetime.Singleton);
            builder.Register<InventoryManager>(Lifetime.Singleton); 

            builder.Register<MainCharacterModel>(Lifetime.Singleton)
                .WithParameter(typeof(Health), new Health(200));
                
            builder.Register<GameplayUIRootViewModel>(Lifetime.Singleton);
            builder.Register<GameplayUIManager>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<GameplayEntryPoint>(Lifetime.Scoped);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}