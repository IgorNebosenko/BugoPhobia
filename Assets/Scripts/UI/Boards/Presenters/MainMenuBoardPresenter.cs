using ElectrumGames.Core.Missions;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ElectrumGames.UI.Boards.Presenters
{
    public class MainMenuBoardPresenter : MonoBehaviour
    {
        [SerializeField] private string discordLink = @"https://discord.gg/j3Ug4MWf6P";
        [SerializeField] private string patreonLink = @"";
        [Space]
        [SerializeField] private BoardsUiController boardsUiController;

        private ViewManager _viewManager;
        private MissionDataHandler _missionDataHandler;
        
        [Inject]
        private void Construct(ViewManager viewManager, MissionDataHandler missionDataHandler)
        {
            _viewManager = viewManager;
            _missionDataHandler = missionDataHandler;
        }

        public void OnSinglePlayerClicked()
        {
            boardsUiController.ShowBoardWithType(DisplayBoardsMenu.SinglePlayer);
        }

        public void OnDemoClicked()
        {
            _missionDataHandler.MissionMap = MissionMap.DemoHouse;
            _missionDataHandler.PlayerJournalId = 0;

            SceneManager.LoadSceneAsync((int)_missionDataHandler.MissionMap);
        }

        public void OnMultiPlayerClicked()
        {
        }

        public void OnTutorialClicked()
        {
        }

        public void OnSettingsClicked()
        {
            _viewManager.ShowView<SettingsPresenter>();
        }

        public void OnAboutClicked()
        {
        }

        public void OnExitClicked()
        {
            Application.Quit();
        }

        public void OnDiscordClicked()
        {
            Application.OpenURL(discordLink);
        }

        public void OnPatreonClicked()
        {
            Application.OpenURL(patreonLink);
        }
    }
}