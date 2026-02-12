using System;
using R3;

namespace Game.UI
{
    public abstract class WindowViewModel : IDisposable
    {
        public Observable<WindowViewModel> CloseRequested => _closeRequested;
        public abstract string Id { get; }
        
        private readonly Subject<WindowViewModel> _closeRequested;

        public void RequestClose()
        {
            _closeRequested.OnNext(this);
        }

        public void Dispose()
        {
            
        }
    }
}