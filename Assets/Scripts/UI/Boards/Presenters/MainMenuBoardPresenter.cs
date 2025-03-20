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
            boardsUiController.ShowBoardWithType(DisplayBoardsMenu.DemoSelector);
        }

        public void OnMultiPlayerClicked()
        {
            boardsUiController.ShowBoardWithType(DisplayBoardsMenu.MultiPlayer);
        }

        public void OnTutorialClicked()
        {
            SceneManager.LoadSceneAsync((int) MissionMap.TutorialSelector);
        }

        public void OnSettingsClicked()
        {
            _viewManager.ShowView<SettingsPresenter>();
        }

        public void OnSpecialThanksClicked()
        {
            boardsUiController.ShowBoardWithType(DisplayBoardsMenu.SpecialThanks);
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