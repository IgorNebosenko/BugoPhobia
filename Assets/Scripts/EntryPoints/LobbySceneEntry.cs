using ElectrumGames.Configs;
using ElectrumGames.Core.Items;
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
        [SerializeField] private ItemSpawnPoint[] itemSpawnPoints;

        private PlayersFactory _playersFactory;
        private ItemsFactory _itemsFactory;
        
        private ViewManager _viewManager;
        private ConfigService _configService;
        
        [Inject]
        private void Construct(PlayersFactory playersFactory, ItemsFactory itemsFactory, ViewManager viewManager, ConfigService configService)
        {
            _playersFactory = playersFactory;
            _itemsFactory = itemsFactory;
            
            _viewManager = viewManager;
            _configService = configService;
        }

        private void Start()
        {
            _configService.FpsConfig = _configService.FpsConfig;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            
            _playersFactory.CreatePlayer(
                true, playerSpawnPoint.position, playerSpawnPoint.rotation);

            for (var i = 0; i < itemSpawnPoints.Length; i++)
                _itemsFactory.Spawn(itemSpawnPoints[i]);

            _viewManager.ShowView<InGamePresenter>();
        }
    }
}