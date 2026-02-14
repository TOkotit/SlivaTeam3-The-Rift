using Game.Gameplay.Root;
using VContainer;
using VContainer.Unity;
using Systems;
using MainCharacter;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace DI
{
    using Game.Gameplay.Root;
    using VContainer;
    using VContainer.Unity;
    using Systems;
    using MainCharacter;
    using UnityEngine;
    using UnityEngine.TextCore.Text;

    namespace DI
    {
        public class GameplayScope: LifetimeScope
        {
            protected override void Configure(IContainerBuilder builder)
            {
                builder.RegisterComponentInHierarchy<MainCharacterCamera>();
                builder.Register<CharacterController>(Lifetime.Scoped);
                builder.RegisterEntryPoint<GameplayEntryPoint>(Lifetime.Scoped);
    
                builder.Register<IGameInputManager, GameInputManager>(Lifetime.Singleton);
    
                builder.RegisterComponentInHierarchy<MainCharacter>()
                    .AsSelf();
                builder.RegisterComponentInHierarchy<CharacterMovement>()
                    .As<IControllable>()      
                    .AsSelf();
                builder.RegisterComponentInHierarchy<MainCharacterInputController>()
                    .AsSelf();
    
                builder.Register<MainCharacterModel>(Lifetime.Singleton);
            }
        }
    }
}