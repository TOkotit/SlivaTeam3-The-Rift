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
        private readonly Subject<Unit> _exitSceneRequest;

        public MainMenuUIManager(IObjectResolver container) : base(container)
        {
            _exitSceneRequest = container.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
        }
        
        public ScreenMainMenuViewModel OpenScreenMainMenu()
        {
            var viewModel = new ScreenMainMenuViewModel(this, _exitSceneRequest);
            var rootUI = Container.Resolve<MainMenuUIRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }
    }
}