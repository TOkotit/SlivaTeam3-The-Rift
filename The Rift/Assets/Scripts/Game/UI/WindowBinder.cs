using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// Абстрактный класс который будет переписываться и цепляться к окнам ui
    /// в качестве T принимает соответствующий view model 
    /// </summary>
    public abstract class WindowBinder<T> : MonoBehaviour, IWindowBinder
        where T : WindowViewModel
    {
        protected T ViewModel;

        public void Bind(WindowViewModel viewModel)
        {
            ViewModel = (T)viewModel;

            OnBind(ViewModel);
        }

        public virtual void Close()
        {
            Destroy(gameObject);
        }
        
        protected virtual void OnBind(T viewModel) {}
    }
}