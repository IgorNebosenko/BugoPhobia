using UnityEngine;

namespace ElectrumGames.UI.Boards.Presenters
{
    public class MainMenuBoardPresenter : MonoBehaviour
    {
        [SerializeField] private string discordLink = @"https://discord.gg/j3Ug4MWf6P";
        [SerializeField] private string patreonLink = @"";
        
        public void OnSinglePlayerClicked()
        {
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