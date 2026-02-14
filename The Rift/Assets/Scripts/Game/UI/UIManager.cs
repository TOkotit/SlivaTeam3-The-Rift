using DI;
using VContainer;

namespace Game.UI
{
    public abstract class UIManager
    {
        // чтобы вытаскивать барахло, чтобы собирать вьюмодели окошек
        protected readonly IObjectResolver Container; 
        
        protected UIManager(IObjectResolver container)
        {
            Container = container;
        }
    }
}