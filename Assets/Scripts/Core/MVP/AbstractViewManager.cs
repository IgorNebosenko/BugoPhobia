using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ElectrumGames.MVP
{
    public abstract class AbstractViewManager
    {
        private const string RegisterMethodName = "RegisterView";
        
        protected delegate Presenter CreateViewDelegate();
        private Dictionary<Type, CreateViewDelegate> _createViewActions = new Dictionary<Type, CreateViewDelegate>();

        private PresenterFactory _factory;
        protected Transform viewContainer;

        protected AbstractViewManager(Transform viewContainer, PresenterFactory factory)
        {
            this.viewContainer = viewContainer;
            _factory = factory;
        }

        protected void RegisterView<TView, TPresenter>()
            where TView : View<TPresenter>
            where TPresenter : Presenter<TView>
        {
            _createViewActions[typeof(TPresenter)] = CreateView<TView, TPresenter>;
        }

        private TPresenter CreateView<TView, TPresenter>()
            where TView : View<TPresenter>
            where TPresenter : Presenter<TView>
        {
            IPresenter presenter = _factory.Create<TPresenter>();
            presenter.Setup(DestroyView);

            return (TPresenter) presenter;
        }

        protected void AutoRegisterViews(List<(Type view, Type presenter)> mvpPairs)
        {
            var registerMethod =
                GetType().GetMethod(RegisterMethodName, BindingFlags.Instance | BindingFlags.NonPublic);
            
            if (registerMethod == null)
                return;

            foreach (var pair in mvpPairs)
            {
                var genRegisterMethod = registerMethod.MakeGenericMethod(pair.view, pair.presenter);
                genRegisterMethod.Invoke(this, new object[] { });
            }
        }

        protected T Create<T>() 
            where T : Presenter
        {
            var keyType = typeof(T);

            if (!_createViewActions.ContainsKey(keyType))
                throw new ViewNotRegisteredException(keyType, this);

            return (T) _createViewActions[keyType]();
        }

        protected abstract void DestroyView(View view);
        
        public override string ToString()
        {
            return $"[{GetType().Name}], Container: {viewContainer?.name}";
        }
    }
}