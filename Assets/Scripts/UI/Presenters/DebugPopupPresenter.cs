using System.Collections.Generic;
using ElectrumGames.Core.Ghost.Controllers;
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
        private readonly GhostController _ghostController;
        
        public DebugPopupPresenter([Inject(Id = "Host")] IPlayer player, GhostController ghostController,
            DebugPopup view) : base(view)
        {
            _player = player;
            _ghostController = ghostController;
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
        
        public void OnResetLogicClicked()
        {
            _ghostController.SetEnabledLogic(GhostLogicSelector.All);
        }

        public void OnForceInteractionClicked()
        {
            _ghostController.NonHuntLogic.ForceInteract();
        }

        public void OnForceGhostEventClicked()
        {
            _ghostController.GhostEventLogic.ForceEvent();
        }

        public void OnForceHuntClicked()
        {
            _player.Sanity.ChangeSanity(-100f, -1);
            _ghostController.HuntLogic.ForceHunt();

        }

        public void OnTeleportToPlayerClicked()
        {
            _ghostController.transform.position = _player.Position;
        }

        public void OnExitClicked()
        {
            Close();
        }
    }
}