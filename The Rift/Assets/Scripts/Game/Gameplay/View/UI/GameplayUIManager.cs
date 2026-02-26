using Game.UI;
using R3;
using VContainer;

namespace Game.Gameplay.View.UI
{
    public class GameplayUIManager : UIManager
    {
        

        public GameplayUIManager(IObjectResolver container) : base(container)
        {
            
        }
        
        public ScreenGameplayViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel(this, Container);
            var rootUI = Container.Resolve<GameplayUIRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }
    }
}