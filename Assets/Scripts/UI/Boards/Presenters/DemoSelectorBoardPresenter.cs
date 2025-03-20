using ElectrumGames.Core.Missions;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ElectrumGames.UI.Boards.Presenters
{
    public class DemoSelectorBoardPresenter : MonoBehaviour
    {
        [SerializeField] private BoardsUiController boardsUiController;
        
        private ViewManager _viewManager;
        private MissionDataHandler _missionDataHandler;
        
        [Inject]
        private void Construct(ViewManager viewManager, MissionDataHandler missionDataHandler)
        {
            _viewManager = viewManager;
            _missionDataHandler = missionDataHandler;
        }

        public void OnFirstHouseClicked()
        {
            _viewManager.ShowView<LoadingPresenter>();
            
            _missionDataHandler.MissionMap = MissionMap.DemoHouse;
            _missionDataHandler.PlayerJournalId = 0;
            
            SceneManager.LoadSceneAsync((int) MissionMap.DemoHouse);
        }
        
        public void OnApartmentHouseClicked()
        {
            _viewManager.ShowView<LoadingPresenter>();
            
            _missionDataHandler.MissionMap = MissionMap.Apartments;
            _missionDataHandler.PlayerJournalId = 0;
            
            SceneManager.LoadSceneAsync((int) MissionMap.Apartments);
        }
        
        public void OnSmallHouseClicked()
        {
            _viewManager.ShowView<LoadingPresenter>();
            
            _missionDataHandler.MissionMap = MissionMap.SmallHouse;
            _missionDataHandler.PlayerJournalId = 0;
            
            SceneManager.LoadSceneAsync((int) MissionMap.SmallHouse);
        }

        public void OnRandomHouseClicked()
        {            
            _viewManager.ShowView<LoadingPresenter>();
            SceneManager.LoadSceneAsync(Random.Range((int)MissionMap.DemoHouse, (int)MissionMap.SmallHouse + 1));
        }

        public void OnBackButtonClicked()
        {
            boardsUiController.ShowBoardWithType(DisplayBoardsMenu.MainMenu);
        }
    }
}