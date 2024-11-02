using System.Collections.Generic;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;
using ElectrumGames.UI.Views;

namespace ElectrumGames.UI.Presenters
{
    public class DebugPopupPresenter : PopupPresenterCoroutine<DebugPopup, PopupArgs, PopupResult>
    {
        public DebugPopupPresenter(DebugPopup view) : base(view)
        {
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            yield break;
        }

        public void OnSanityFullClicked()
        {
        }

        public void OnSanityEmptyClicked()
        {
        }

        public void OnAddSanityClicked()
        {
        }

        public void OnRemoveSanityClicked()
        {
        }

        public void OnForceInteractionClicked()
        {
        }

        public void OnForceGhostEventClicked()
        {
        }

        public void OnForceHuntClicked()
        {
        }

        public void OnTeleportToPlayerClicked()
        {
        }

        public void OnExitClicked()
        {
        }
    }
}