using System;
using System.Collections.Generic;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;
using ElectrumGames.UI.Views;

namespace ElectrumGames.UI.Presenters
{
    public class SpiritBoxPopupPresenter : PopupPresenterCoroutine<SpiritBoxPopup, PopupArgs, PopupResult>
    {
        private Action _onClose;
        
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
        }

        public void OnIsMaleClicked()
        {
        }

        public void OnAgeClicked()
        {
        }

        public void OnExitClicked()
        {
            _onClose?.Invoke();
            Close();
        }
    }
}