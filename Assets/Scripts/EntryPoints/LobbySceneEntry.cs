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
            Cursor.visible = false;
            
            _playersFactory.CreatePlayer(
                true, playerSpawnPoint.position, playerSpawnPoint.rotation);

            //ToDo make it from config!
            _itemsFactory.Spawn(ItemType.FlashLightSmall, new Vector3(-1f, 1f, 1f), Quaternion.identity);

            _viewManager.ShowView<InGamePresenter>();
        }
    }
}