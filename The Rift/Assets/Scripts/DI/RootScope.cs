using Unity.VisualScripting;
using UnityEngine;
using Utils;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class RootScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            
            var coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            DontDestroyOnLoad(coroutines);
            builder.RegisterInstance<ICoroutineRunner>(coroutines);

            
            builder.RegisterEntryPoint<Root.EntryPoint>(Lifetime.Singleton);
            
        }
    }
}