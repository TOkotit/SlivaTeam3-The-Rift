using UnityEngine;
using Utils;
using VContainer;
using VContainer.Unity;

namespace Root
{
    public class EntryPoint : IStartable
    {
        private Coroutines _coroutines;
        private IObjectResolver _resolver;
        
        public void Start()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            _resolver.Inject(_coroutines);
            RunGame();
        }

        private EntryPoint(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
            
        public void RunGame()
        {
            
        }
    }
}