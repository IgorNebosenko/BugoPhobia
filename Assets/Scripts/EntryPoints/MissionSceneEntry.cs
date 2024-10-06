using ElectrumGames.Configs;
using ElectrumGames.Core.Environment.House;
using ElectrumGames.Core.Ghost;
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
        
        [Inject]
        private void Construct(PlayersFactory playersFactory, GhostFactory ghostFactory,
            ViewManager viewManager, ConfigService configService, HouseController houseController)
        {
            _playersFactory = playersFactory;
            _ghostFactory = ghostFactory;
            
            _viewManager = viewManager;
            _configService = configService;

            _houseController = houseController;
        }
        
        private void Start()
        {
            _configService.FpsConfig = _configService.FpsConfig;
            
            _playersFactory.CreatePlayer(
                true, playerSpawnPoints[0].position, playerSpawnPoints[0].rotation);

            _ghostFactory.CreateGhost(_houseController.Rooms);

#if UNITY_EDITOR || UNITY_STANDALONE
            _viewManager.ShowView<InGamePresenter>();
#elif UNITY_ANDROID
            _viewManager.ShowView<InGameAndroidPresenter>();
#endif
        }
    }
}