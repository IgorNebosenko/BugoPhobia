using ElectrumGames.Configs;
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
        
        private ViewManager _viewManager;
        private ConfigService _configService;
        
        [Inject]
        private void Construct(PlayersFactory playersFactory, ViewManager viewManager, ConfigService configService)
        {
            _playersFactory = playersFactory;
            
            _viewManager = viewManager;
            _configService = configService;
        }
        
        private void Start()
        {
            _configService.FpsConfig = _configService.FpsConfig;
            
            _playersFactory.CreatePlayer(
                true, playerSpawnPoints[0].position, playerSpawnPoints[0].rotation);

            _viewManager.ShowView<InGamePresenter>();
        }
    }
}