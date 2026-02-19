using Game.Gameplay.View.UI;
using Game.Gameplay.View.UI.PopupA;
using Game.MainMenu.View.UI.ScreenMainMenu;
using Game.UI;
using R3;
using VContainer;

namespace Game.MainMenu.View.UI
{
    public class MainMenuUIManager : UIManager
    {
        

        public MainMenuUIManager(IObjectResolver container) : base(container)
        {
            
        }
        
        public ScreenMainMenuViewModel OpenScreenMainMenu()
        {
            var viewModel = new ScreenMainMenuViewModel(this, Container);
            var rootUI = Container.Resolve<MainMenuUIRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }
    }
}