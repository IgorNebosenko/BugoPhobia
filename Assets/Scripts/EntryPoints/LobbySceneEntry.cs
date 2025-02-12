using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost;
using ElectrumGames.Core.Lobby;
using ElectrumGames.Core.Player;
using ElectrumGames.GlobalEnums;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using Zenject;

namespace ElectrumGames.EntryPoints
{
    public class LobbySceneEntry : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;
        
        private PlayersFactory _playersFactory;
        
        private ViewManager _viewManager;
        private ConfigService _configService;

        private MissionResultHandler _missionResultHandler;
        private GhostEnvironmentHandler _ghostEnvironmentHandler;
        
        [Inject]
        private void Construct(PlayersFactory playersFactory, ViewManager viewManager, ConfigService configService, 
            MissionResultHandler missionResultHandler, GhostEnvironmentHandler ghostEnvironmentHandler)
        {
            _playersFactory = playersFactory;
            
            _viewManager = viewManager;
            _configService = configService;
            _missionResultHandler = missionResultHandler;

            _ghostEnvironmentHandler = ghostEnvironmentHandler;
        }

        private void Start()
        {
            _configService.FpsConfig = _configService.FpsConfig;

            _configService.MusicVolume = _configService.MusicVolume;
            _configService.SoundsVolume = _configService.SoundsVolume;
            
            Debug.LogWarning("All player creates as host and playable!");
            _playersFactory.CreatePlayer(
                true, true, playerSpawnPoint.position, playerSpawnPoint.rotation);
            
            Debug.LogWarning("need to set result of 1-3 missions pass!");
            _missionResultHandler.OnLobbyEnter(_ghostEnvironmentHandler.GhostVariables.ghostType, 
                MissionsUnion.Empty());
            
#if UNITY_STANDALONE
            _viewManager.ShowView<InGamePresenter>();
#elif UNITY_ANDROID
            _viewManager.ShowView<InGameAndroidPresenter>();
#endif
        }
    }
}