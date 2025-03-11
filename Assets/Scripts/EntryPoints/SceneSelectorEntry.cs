using ElectrumGames.Core.Lobby;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Tutorial;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ElectrumGames.EntryPoints
{
    public class SceneSelectorEntry : MonoBehaviour
    {
        private TutorialHandler _tutorialHandler;
        private LobbyManager _lobbyManager;
        private ViewManager _viewManager;
        
        [Inject]
        private void Construct(TutorialHandler tutorialHandler, LobbyManager lobbyManager, ViewManager viewManager)
        {
            _tutorialHandler = tutorialHandler;
            _lobbyManager = lobbyManager;
            _viewManager = viewManager;
        }

        private void Start()
        {
            if (!_tutorialHandler.IsTutorialFinished)
            {
                _viewManager.ShowView<LoadingPresenter>();
                SceneManager.LoadSceneAsync((int) MissionMap.TutorialSelector);
            }
            else
            {
                _viewManager.ShowView<LoadingPresenter>();
                SceneManager.LoadSceneAsync((int) MissionMap.LobbyTier0 + _lobbyManager.LobbyId);
            }
        }
    }
}