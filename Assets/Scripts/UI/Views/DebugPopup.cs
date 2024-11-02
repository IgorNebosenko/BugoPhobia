using ElectrumGames.MVP;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Views
{
    [AutoRegisterView("Views/Popups/DebugPopup")]
    public class DebugPopup : View<DebugPopupPresenter>
    {
        [SerializeField] private Button sanityFullButton;
        [SerializeField] private Button sanityEmptyButton;
        [SerializeField] private Button sanityAddButton;
        [SerializeField] private Button sanityRemoveButton;
        [Space]
        [SerializeField] private Button forceInteractionButton;
        [SerializeField] private Button forceGhostEventButton;
        [SerializeField] private Button forceHuntButton;
        [SerializeField] private Button teleportToPlayer;
        [Space]
        [SerializeField] private Button exitButton;

        private void Start()
        {
            sanityFullButton.onClick.AddListener(Presenter.OnSanityFullClicked);
            sanityEmptyButton.onClick.AddListener(Presenter.OnSanityEmptyClicked);
            sanityAddButton.onClick.AddListener(Presenter.OnAddSanityClicked);
            sanityRemoveButton.onClick.AddListener(Presenter.OnRemoveSanityClicked);
            
            forceInteractionButton.onClick.AddListener(Presenter.OnForceInteractionClicked);
            forceGhostEventButton.onClick.AddListener(Presenter.OnForceGhostEventClicked);
            forceHuntButton.onClick.AddListener(Presenter.OnForceHuntClicked);
            teleportToPlayer.onClick.AddListener(Presenter.OnTeleportToPlayerClicked);
            
            exitButton.onClick.AddListener(Presenter.OnExitClicked);
        }

        private void OnDestroy()
        {
            sanityFullButton.onClick.RemoveListener(Presenter.OnSanityFullClicked);
            sanityEmptyButton.onClick.RemoveListener(Presenter.OnSanityEmptyClicked);
            sanityAddButton.onClick.RemoveListener(Presenter.OnAddSanityClicked);
            sanityRemoveButton.onClick.RemoveListener(Presenter.OnRemoveSanityClicked);
            
            forceInteractionButton.onClick.RemoveListener(Presenter.OnForceInteractionClicked);
            forceGhostEventButton.onClick.RemoveListener(Presenter.OnForceGhostEventClicked);
            forceHuntButton.onClick.RemoveListener(Presenter.OnForceHuntClicked);
            teleportToPlayer.onClick.RemoveListener(Presenter.OnTeleportToPlayerClicked);
            
            exitButton.onClick.RemoveListener(Presenter.OnExitClicked);
        }
    }
}