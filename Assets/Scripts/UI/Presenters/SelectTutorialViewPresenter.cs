using ElectrumGames.Core.Lobby;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Tutorial;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Views;
using UnityEngine.SceneManagement;

namespace ElectrumGames.UI.Presenters
{
    public class SelectTutorialViewPresenter : Presenter<SelectTutorialView>
    {
        private readonly LobbyManager _lobbyManager;
        private readonly TutorialHandler _tutorialHandler;
        private readonly ViewManager _viewManager;
        
        public SelectTutorialViewPresenter(SelectTutorialView view, LobbyManager lobbyManager, 
            TutorialHandler tutorialHandler, ViewManager viewManager) :
            base(view)
        {
            _lobbyManager = lobbyManager;
            _tutorialHandler = tutorialHandler;
            _viewManager = viewManager;
        }

        public void OnYesButtonClicked()
        {
            _tutorialHandler.IsTutorialFinished = true;
            SceneManager.LoadSceneAsync((int) MissionMap.TutorialFull);
            _viewManager.ShowView<LoadingPresenter>();
        }

        public void OnAlreadyPlayedButtonClicked()
        {
            _tutorialHandler.IsTutorialFinished = true;
            SceneManager.LoadSceneAsync((int) MissionMap.TutorialShort);
            _viewManager.ShowView<LoadingPresenter>();
        }

        public void OnSkipButtonClicked()
        {
            SceneManager.LoadSceneAsync((int) MissionMap.LobbyTier0 + _lobbyManager.LobbyId);

            _tutorialHandler.IsTutorialFinished = true;
            
            _viewManager.ShowView<LoadingPresenter>();
        }
    }
}