using ElectrumGames.MVP;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Views
{
    [AutoRegisterView]
    public class SelectTutorialView : View<SelectTutorialViewPresenter>
    {
        [SerializeField] private Button yesButton;
        [SerializeField] private Button alreadyPlayButton;
        [SerializeField] private Button skipButton;

        private void Start()
        {
            yesButton.onClick.AddListener(Presenter.OnYesButtonClicked);
            alreadyPlayButton.onClick.AddListener(Presenter.OnAlreadyPlayedButtonClicked);
            skipButton.onClick.AddListener(Presenter.OnSkipButtonClicked);
        }

        private void OnDestroy()
        {
            yesButton.onClick.RemoveListener(Presenter.OnYesButtonClicked);
            alreadyPlayButton.onClick.RemoveListener(Presenter.OnAlreadyPlayedButtonClicked);
            skipButton.onClick.RemoveListener(Presenter.OnSkipButtonClicked);
        }
    }
}