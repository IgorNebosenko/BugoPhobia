using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
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
        
        [Inject]
        private void Construct(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        public void OnSinglePlayerClicked()
        {
            boardsUiController.ShowBoardWithType(DisplayBoardsMenu.SinglePlayer);
        }

        public void OnDemoClicked()
        {
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