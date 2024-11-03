using System.Collections.Generic;
using ElectrumGames.Core.Player;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;
using ElectrumGames.UI.Views;
using Zenject;

namespace ElectrumGames.UI.Presenters
{
    public class DebugPopupPresenter : PopupPresenterCoroutine<DebugPopup, PopupArgs, PopupResult>
    {
        private readonly IPlayer _player;
        
        public DebugPopupPresenter([Inject(Id = "Host")] IPlayer player, DebugPopup view) : base(view)
        {
            _player = player;
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            yield break;
        }

        public void OnSanityFullClicked()
        {
            _player.Sanity.ChangeSanity(100f, -1);
        }

        public void OnSanityEmptyClicked()
        {
            _player.Sanity.ChangeSanity(-100f, -1);
        }

        public void OnAddSanityClicked()
        {
            _player.Sanity.ChangeSanity(25f, -1);
        }

        public void OnRemoveSanityClicked()
        {
            _player.Sanity.ChangeSanity(-25f, -1);
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
            Close();
        }
    }
}