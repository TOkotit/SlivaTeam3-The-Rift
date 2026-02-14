using Game.Gameplay.View.UI;
using R3;
using Systems;
using UIRoot;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Gameplay.Root
{
    public class GameplayEntryPoint : IStartable
    {
        private GameplayUIRootBinder _sceneUIRootPrefab;
        
        readonly IGameManager _gameManager;
        
        public GameplayEntryPoint(IObjectResolver resolver)
        {
            _gameManager =  resolver.Resolve<IGameManager>();
            
            InitUI(resolver);

            var exitSceneRequest = resolver.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
            var exitToMainMenuSceneSignal = exitSceneRequest.Select(_ => Unit.Default);
        }
        
        public void Start()
        {
            Debug.Log("GameplayEntryPoint.Start");
            
            _gameManager.SetState(GameState.Gameplay);
        }
        

        private void InitUI(IObjectResolver resolver)
        {
            // Создали UI для сцены (это было)
            var uiRoot = resolver.Resolve<UIRootView>();
            var uiSceneRootBinder = resolver.Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);
            
            // Запрашиваем рутовую вью модель и пихаем ее в баиндер, который создали
            var uiSceneRootViewModel = resolver.Resolve<GameplayUIRootViewModel>();
            uiSceneRootBinder.Bind(uiSceneRootViewModel);
            
            // можно открывать окошки
            var uiManager = resolver.Resolve<GameplayUIManager>();
            uiManager.OpenScreenGameplay();
        }

        
    }
}