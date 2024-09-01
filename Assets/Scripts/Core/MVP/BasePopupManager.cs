using System;
using System.Collections.Generic;
using UnityEngine;

namespace ElectrumGames.MVP
{
    public class BasePopupManager : AbstractViewManager
    {
        public Action<Presenter> OnViewShown;

        private List<Presenter> _popups = new List<Presenter>();
        public IReadOnlyList<Presenter> Popups => _popups;

        private readonly Queue<Action> _popupQueue = new Queue<Action>();
        private List<(int order, Presenter popup)> _orderPopups = new List<(int order, Presenter popup)>();

        protected BasePopupManager(Transform viewContainer, PresenterFactory factory) : base(viewContainer, factory)
        {
        }

        public TPresenter ShowPopup<TPresenter>(int order = 0) where TPresenter : Presenter
        {
            var presenter = Create<TPresenter>();
            presenter.BaseView.transform.SetParent(viewContainer, false);
            _popups.Add(presenter);

            var index = 0;
            for (index = 0; index < _orderPopups.Count; index++)
            {
                if (order >= _orderPopups[index].order)
                    break;
            }
            
            presenter.BaseView.transform.SetSiblingIndex(viewContainer.childCount - index - 1);
            _orderPopups.Insert(index, (order, presenter));

            presenter.OnClose += p =>
            {
                _popups.Remove(presenter);
                _orderPopups.RemoveAll(op => op.popup == p);
            };
            
            OnViewShown?.Invoke(presenter);
            return presenter;
        }
        
        //Can has realization for async tasks

        protected override void DestroyView(View view)
        {
            UnityEngine.Object.Destroy(view.gameObject);
        }
    }
}