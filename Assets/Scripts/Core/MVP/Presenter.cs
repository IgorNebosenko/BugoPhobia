using System;
using System.Collections.Generic;
using UnityEngine;

#if ELECTRUM_CORE
using ElectrumGames.Core;
#else
using ElectrumGames.MVP.Utils;
#endif

namespace ElectrumGames.MVP
{
    public interface IPresenter
    {
        event Action<IPresenter> OnClose;
        void Setup(Action<View> closeAction);
        IEnumerator<Transform> SubviewContainer { get; }
        View BaseView { get; }
    }

    public abstract class Presenter : IDisposablesContainer, IDisposable, IPresenter
    {
        protected List<IDisposable> disposables = new List<IDisposable>();
        private View _baseView;
        private Action<View> _viewClosing;

        public View BaseView
        {
            get
            {
                if (_baseView == null)
                    throw new Exception("View not created or already destroyed!");
                return _baseView;
            }
        }

        public IEnumerator<Transform> SubviewContainer => _baseView.SubviewContainer;
        
        protected Presenter(View view)
        {
            _baseView = view;
        }
        
        public event Action<IPresenter> OnClose;
        
        public void Setup(Action<View> closeAction)
        {
            _viewClosing = closeAction;
            Init();
            _baseView.Init(this);
            _baseView.Show();
        }

        protected virtual void Init()
        {
        }

        protected virtual void Closing()
        {
        }

        private void ViewClosed()
        {
            ((IDisposable)this).Dispose();
            _baseView = null;
        }

        void IDisposable.Dispose()
        {
            for (var i = 0; i < disposables.Count; i++)
                disposables[i].Dispose();
            
            disposables.Clear();
        }

        public void RegisterForDispose(IDisposable disposable)
        {
            disposables.Add(disposable);
        }

        public void Close()
        {
            if (!_baseView)
                throw new Exception("View not created or already destroyed");
            
            Closing();
            _baseView.Hide();
            _viewClosing.Invoke(_baseView);
            ViewClosed();
            OnClose?.Invoke(this);
        }
    }

    public abstract class Presenter<TView> : Presenter where TView : View
    {
        private TView _view;

        protected TView View
        {
            get
            {
                if (!_view)
                    throw new Exception("View (" + typeof(TView) + ") not created or already destroyed");
                return _view;
            }
        }

        public bool ViewExists => _view != null;

        protected Presenter(TView view) : base(view)
        {
            _view = view;
        }
    }
}