using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

#if ELECTRUM_CORE
using ElectrumGames.Core;
#else
using ElectrumGames.MVP.Utils;
#endif

namespace ElectrumGames.MVP
{
    public class BaseViewManager : AbstractViewManager
    {
        public event Action<Presenter> OnViewShown;

        private readonly HierarchyTree<Presenter> _viewHierarchy = new HierarchyTree<Presenter>();
        internal IReadonlyHierarchyTree<Presenter> Hierarchy => _viewHierarchy;

        protected BaseViewManager(Transform viewContainer, PresenterFactory factory) : base(viewContainer, factory)
        {
        }

        public Presenter FirstOrDefaultAtHierarchy(Func<Presenter, bool> predicate)
        {
            return _viewHierarchy.FirstOrDefault(predicate);
        }

        public TPresenter ShowView<TPresenter>() where TPresenter : Presenter
        {
            CloseRootView();
            var view = CreateView<TPresenter>();
            _viewHierarchy.SetRoot(view);
            OnViewShown?.Invoke(view);
            return view;
        }

        public TPresenter ShowSubView<TPresenter>(Presenter parent) where TPresenter : Presenter
        {
            var view = CreateView<TPresenter>(parent);
            _viewHierarchy.AddItem(view, parent);
            OnViewShown?.Invoke(view);
            return view;
        }

        public void CloseRootView()
        {
            _viewHierarchy.Root?.Close();
        }

        public void CloseSubViews(Presenter view)
        {
            var subs = _viewHierarchy.GetSubItemsOf(view);
            
            while (subs.Count > 0)
                subs[0].Close();
        }

        private Transform GetViewContainer(IPresenter parentView)
        {
            if (parentView == null)
                return viewContainer;

            if (parentView.SubviewContainer == null)
                return viewContainer;

            parentView.SubviewContainer.MoveNext();

            var container = parentView.SubviewContainer.Current;
            return container ? container : viewContainer;
        }

        private TPresenter CreateView<TPresenter>(Presenter parentView = null)
            where TPresenter : Presenter
        {
            var container = GetViewContainer(parentView);

            var presenter = Create<TPresenter>();
            IPresenter interfacePresenter = presenter;
            
            interfacePresenter.BaseView.transform.SetParent(container, false);
            interfacePresenter.BaseView.transform.SetAsLastSibling();

            presenter.OnClose += p =>
            {
                CloseSubViews((Presenter)p);
                _viewHierarchy.RemoveItem(presenter);
            };

            return presenter;
        }

        protected override void DestroyView(View view)
        {
            Object.Destroy(view.gameObject);
        }
    }
}