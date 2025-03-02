using ElectrumGames.MVP;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Views
{
    [AutoRegisterView("Views/Popups/SpiritBoxPopup")]
    public class SpiritBoxPopup : View<SpiritBoxPopupPresenter>
    {
        [SerializeField] private Button whereAreYouButton;
        [SerializeField] private Button areYouMaleButton;
        [SerializeField] private Button ageButton;
        [Space]
        [SerializeField] private Button closeButton;

        private void Start()
        {
            whereAreYouButton.onClick.AddListener(Presenter.OnWhereAreYouClicked);
            areYouMaleButton.onClick.AddListener(Presenter.OnIsMaleClicked);
            ageButton.onClick.AddListener(Presenter.OnAgeClicked);
            
            closeButton.onClick.AddListener(Presenter.OnExitClicked);
        }

        private void OnDestroy()
        {
            whereAreYouButton.onClick.RemoveListener(Presenter.OnWhereAreYouClicked);
            areYouMaleButton.onClick.RemoveListener(Presenter.OnIsMaleClicked);
            ageButton.onClick.RemoveListener(Presenter.OnAgeClicked);
            
            closeButton.onClick.RemoveListener(Presenter.OnExitClicked);
        }
    }
}