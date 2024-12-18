using ElectrumGames.Configs;
using ElectrumGames.Core.Environment.House;
using ElectrumGames.Core.Ghost;
using ElectrumGames.Core.Journal;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using Zenject;

namespace ElectrumGames.EntryPoints
{
    public class MissionSceneEntry : MonoBehaviour
    {
        [SerializeField] protected Transform[] playerSpawnPoints;
        
        private PlayersFactory _playersFactory;
        private GhostFactory _ghostFactory;
        
        private ViewManager _viewManager;
        private ConfigService _configService;

        private HouseController _houseController;

        private MissionPlayersHandler _missionPlayersHandler;

        private JournalManager _journalManager;
        
        [Inject]
        private void Construct(PlayersFactory playersFactory, GhostFactory ghostFactory,
            ViewManager viewManager, ConfigService configService, HouseController houseController, 
            MissionPlayersHandler missionPlayersHandler, JournalManager journalManager)
        {
            _playersFactory = playersFactory;
            _ghostFactory = ghostFactory;
            
            _viewManager = viewManager;
            _configService = configService;

            _houseController = houseController;

            _missionPlayersHandler = missionPlayersHandler;

            _journalManager = journalManager;
        }
        
        private void Start()
        {
            _configService.FpsConfig = _configService.FpsConfig;
            
            Debug.LogWarning("All player creates as host and playable!");
            var player = _playersFactory.CreatePlayer(
                true, true, playerSpawnPoints[0].position, playerSpawnPoints[0].rotation);

            _missionPlayersHandler.ConnectPlayer(player);

            _ghostFactory.CreateGhost(_houseController.Rooms);
            
            _journalManager.PlayerJournalInstance.Reset();
            
            for (var i = 0; i < _journalManager.OtherPlayersJournalInstances.Count; i++)
                _journalManager.OtherPlayersJournalInstances[i].Reset();

#if UNITY_STANDALONE
            _viewManager.ShowView<InGamePresenter>();
#elif UNITY_ANDROID
            _viewManager.ShowView<InGameAndroidPresenter>();
#endif
        }
    }
}