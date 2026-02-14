using Game.Gameplay.View.UI.PopupA;
using Game.UI;
using R3;
using VContainer;

namespace Game.Gameplay.View.UI
{
    public class GameplayUIManager : UIManager
    {
        private readonly Subject<Unit> _exitSceneRequest;

        public GameplayUIManager(IObjectResolver container) : base(container)
        {
            _exitSceneRequest = container.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
        }
        
        public ScreenGameplayViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel(this, _exitSceneRequest);
            var rootUI = Container.Resolve<GameplayUIRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }

        public PopupAViewModel OpenPopupA()
        {
            var a = new PopupAViewModel();
            var rootUI = Container.Resolve<GameplayUIRootViewModel>();

            rootUI.OpenPopup(a);

            return a;
        }
    }
}