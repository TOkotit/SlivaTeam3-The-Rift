using Game.UI;
using R3;
using VContainer;

namespace Game.Gameplay.View.UI
{
    public class GameplayUIManager : UIManager
    {
        // Доп контейнер чтобы получать инфу именно о персонаже

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