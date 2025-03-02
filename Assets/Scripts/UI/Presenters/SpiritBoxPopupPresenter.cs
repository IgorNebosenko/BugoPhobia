using System.Collections.Generic;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;
using ElectrumGames.UI.Views;

namespace ElectrumGames.UI.Presenters
{
    public class SpiritBoxPopupPresenter : PopupPresenterCoroutine<SpiritBoxPopup, PopupArgs, PopupResult>
    {
        public SpiritBoxPopupPresenter(SpiritBoxPopup view) : base(view)
        {
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            yield break;
        }
    }
}