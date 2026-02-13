using VContainer;
using VContainer.Unity;
using Systems;
namespace DI
{
    public class GameplayScope: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IInputManager, InputManager>(Lifetime.Singleton);
        }
    }
}