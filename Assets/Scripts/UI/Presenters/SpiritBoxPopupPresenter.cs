using System;
using System.Collections.Generic;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;
using ElectrumGames.UI.Views;
using UnityEngine;

namespace ElectrumGames.UI.Presenters
{
    public class SpiritBoxPopupPresenter : PopupPresenterCoroutine<SpiritBoxPopup, PopupArgs, PopupResult>
    {
        private Action _onClose;

        public event Action WhereAreYouClicked;
        public event Action IsMaleClicked;
        public event Action AgeClicked;
        
        
        public SpiritBoxPopupPresenter(SpiritBoxPopup view) : base(view)
        {
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            yield break;
        }

        public void Init(Action onClose)
        {
            _onClose = onClose;
        }

        public void OnWhereAreYouClicked()
        {
            WhereAreYouClicked?.Invoke();
        }

        public void OnIsMaleClicked()
        {
            IsMaleClicked?.Invoke();
        }

        public void OnAgeClicked()
        {
            AgeClicked?.Invoke();
        }

        public void OnExitClicked()
        {
            _onClose?.Invoke();
            Close();
        }
    }
}