using ElectrumGames.UI.Boards.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Boards.Views
{
    public class MainMenuBoardView : MonoBehaviour
    {
        [SerializeField] private Button singlePlayerButton;
        [SerializeField] private Button demoButton;
        [SerializeField] private Button multiPlayerButton;
        [SerializeField] private Button tutorialButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button aboutUsButton;
        [SerializeField] private Button exitButton;
        [Space]
        [SerializeField] private Button discordButton;
        [SerializeField] private Button patreonButton;
        [Space]
        [SerializeField] private TMP_Text versionText;
        [Space]
        [SerializeField] private MainMenuBoardPresenter presenter;

        private void Awake()
        {
            versionText.text = Application.version;
        }

        private void OnEnable()
        {
            singlePlayerButton.onClick.AddListener(presenter.OnSinglePlayerClicked);
            demoButton.onClick.AddListener(presenter.OnDemoClicked);
            multiPlayerButton.onClick.AddListener(presenter.OnMultiPlayerClicked);
            tutorialButton.onClick.AddListener(presenter.OnTutorialClicked);
            settingsButton.onClick.AddListener(presenter.OnSettingsClicked);
            aboutUsButton.onClick.AddListener(presenter.OnAboutClicked);
            exitButton.onClick.AddListener(presenter.OnExitClicked);
            
            discordButton.onClick.AddListener(presenter.OnDiscordClicked);
            patreonButton.onClick.AddListener(presenter.OnPatreonClicked);
        }
        
        private void OnDisable()
        {
            singlePlayerButton.onClick.RemoveListener(presenter.OnSinglePlayerClicked);
            demoButton.onClick.RemoveListener(presenter.OnDemoClicked);
            multiPlayerButton.onClick.RemoveListener(presenter.OnMultiPlayerClicked);
            tutorialButton.onClick.RemoveListener(presenter.OnTutorialClicked);
            settingsButton.onClick.RemoveListener(presenter.OnSettingsClicked);
            aboutUsButton.onClick.RemoveListener(presenter.OnAboutClicked);
            exitButton.onClick.RemoveListener(presenter.OnExitClicked);
            
            discordButton.onClick.RemoveListener(presenter.OnDiscordClicked);
            patreonButton.onClick.RemoveListener(presenter.OnPatreonClicked);
        }
    }
}
