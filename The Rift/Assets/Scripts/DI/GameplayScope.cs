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
            builder.Register<GameData>(Lifetime.Singleton);
            builder.Register<IGameInputManager, GameInputManager>(Lifetime.Singleton);
                
            builder.Register<Inventory>(Lifetime.Singleton);
            builder.Register<InventoryManager>(Lifetime.Singleton);
            
            
            builder.Register<Health>(Lifetime.Scoped);
            builder.Register<Stamina>(Lifetime.Scoped);
            builder.Register<MainCharacterModel>(Lifetime.Singleton);

                
            builder.Register<GameplayUIRootViewModel>(Lifetime.Singleton);
            builder.Register<GameplayUIManager>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<GameplayEntryPoint>(Lifetime.Scoped);
            // Cursor.visible = false;
            // Cursor.lockState = CursorLockMode.Locked;
        }
    }
}