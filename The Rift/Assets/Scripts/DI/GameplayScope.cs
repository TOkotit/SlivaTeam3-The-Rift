using Game;
using Game.Gameplay.Root;
using R3;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameplayScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            
            builder.RegisterInstance(new Subject<Unit>())
                .Keyed(AppConstants.EXIT_SCENE_REQUEST_TAG);
            
            
            builder.RegisterEntryPoint<GameplayEntryPoint>();
        }
    }
}