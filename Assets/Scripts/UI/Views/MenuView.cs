using ElectrumGames.MVP;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Views
{
    [AutoRegisterView]
    public class MenuView : View<MenuPresenter>
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;

        private void Start()
        {
            continueButton.onClick.AddListener(Presenter.OnContinueButtonClicked);
            settingsButton.onClick.AddListener(Presenter.OnSettingsButtonClicked);
            exitButton.onClick.AddListener(Presenter.OnExitButtonClicked);
        }

        private void OnDestroy()
        {
            continueButton.onClick.RemoveListener(Presenter.OnContinueButtonClicked);
            settingsButton.onClick.RemoveListener(Presenter.OnSettingsButtonClicked);
            exitButton.onClick.RemoveListener(Presenter.OnExitButtonClicked);
        }
    }
}