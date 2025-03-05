using ElectrumGames.Core.Missions;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElectrumGames.UI.Presenters
{
    public class SelectTutorialViewPresenter : Presenter<SelectTutorialView>
    {
        private readonly ViewManager _viewManager;
        
        public SelectTutorialViewPresenter(SelectTutorialView view, ViewManager viewManager) : base(view)
        {
            _viewManager = viewManager;
        }

        public void OnYesButtonClicked()
        {
            SceneManager.LoadSceneAsync((int) MissionMap.TutorialFull);
            _viewManager.ShowView<LoadingPresenter>();
        }

        public void OnAlreadyPlayedButtonClicked()
        {
            SceneManager.LoadSceneAsync((int) MissionMap.TutorialShort);
            _viewManager.ShowView<LoadingPresenter>();
        }

        public void OnSkipButtonClicked()
        {
            SceneManager.LoadSceneAsync((int) MissionMap.LobbyTier0);
            Debug.LogError("Here must be selection lobby not 0!");
            _viewManager.ShowView<LoadingPresenter>();
        }
    }
}