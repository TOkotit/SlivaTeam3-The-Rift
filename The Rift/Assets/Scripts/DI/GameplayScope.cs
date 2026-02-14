using Game;
using Game.Gameplay.Root;
using Game.Gameplay.View.UI;
using R3;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameplayScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameplayUIRootViewModel>(Lifetime.Singleton);
            builder.Register<GameplayUIManager>(Lifetime.Singleton);
            
            builder.RegisterInstance(new Subject<Unit>())
                .Keyed(AppConstants.EXIT_SCENE_REQUEST_TAG);
            
            
            builder.RegisterEntryPoint<GameplayEntryPoint>();
        }
    }
}