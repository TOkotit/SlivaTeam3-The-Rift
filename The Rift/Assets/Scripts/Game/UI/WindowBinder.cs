using UnityEngine;

namespace Game.UI
{
    public abstract class WindowBinder<T> : MonoBehaviour, IWindowBinder
        where T : WindowViewModel
    {
        protected T _viewModel;

        public void Bind(WindowViewModel viewModel)
        {
            _viewModel = (T)viewModel;

            OnBind(_viewModel);
        }

        public virtual void Close(T viewModel)
        {
            //здесь анимации закрытия
            Destroy(gameObject);
        }
        
        protected virtual void OnBind(T viewModel) {}
    }
}