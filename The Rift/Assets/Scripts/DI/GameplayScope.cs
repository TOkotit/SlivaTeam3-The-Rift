using Entity;
using Game.Gameplay.Root;
using VContainer;
using VContainer.Unity;
using Systems;
using MainCharacter;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Game;
using Game.Gameplay.Root;
using Game.Gameplay.View.UI;
using R3;
using Unity.VisualScripting;
using Utils;
using VContainer;
using VContainer.Unity;

namespace DI
{
        public class GameplayScope: LifetimeScope
        {
            protected override void Configure(IContainerBuilder builder)
            {
                builder.Register<DamagableRegistry>(Lifetime.Singleton);
                builder.Register<AttackSystem>(Lifetime.Singleton);
                
                var coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>(); //удалить потом
                DontDestroyOnLoad(coroutines.gameObject);
                builder.RegisterInstance<ICoroutineRunner>(coroutines);
                
                builder.RegisterComponentInHierarchy<MainCharacterAttackController>();
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
                builder.RegisterComponentInHierarchy<MainCharacterMovementController>()
                    .AsSelf();
                
                builder.Register<MainCharacterModel>(Lifetime.Singleton)
                    .WithParameter(typeof(Health), new Health(200));
                
                builder.RegisterEntryPoint<GameplayEntryPoint>(Lifetime.Scoped);

            }
        }
}