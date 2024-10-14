using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Network;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Player
{
    public class PlayersFactory : MonoBehaviour, IHaveNetId
    {
        [SerializeField] private Player playerTemplate;
        
        private NetIdFactory _netIdFactory;
        private PlayerConfig _playerConfig;
        private InputActions _inputActions;
        private ConfigService _configService;
        private ItemsConfig _itemsConfig;
        private GhostDifficultyList _difficultyList;

        private Camera _playerCamera;

        private MissionDataHandler _missionDataHandler;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }
        
        [Inject]
        private void Construct(NetIdFactory netIdFactory, PlayerConfig playerConfig, InputActions inputActions,
            ConfigService configService, ItemsConfig itemsConfig, GhostDifficultyList difficultyList, Camera playerCamera,
            MissionDataHandler missionDataHandler)
        {
            _netIdFactory = netIdFactory;
            _playerConfig = playerConfig;
            _inputActions = inputActions;
            _configService = configService;
            _itemsConfig = itemsConfig;
            _difficultyList = difficultyList;

            _playerCamera = playerCamera;
            
            _missionDataHandler = missionDataHandler;

            _netIdFactory.Initialize(this);
        }
        
        public IPlayer CreatePlayer(bool isHost, Vector3 position, Quaternion rotation)
        {
            var player = Instantiate(playerTemplate, position, rotation, transform);
            player.Spawn(_playerConfig, _configService, isHost, _inputActions, _itemsConfig, 
                _difficultyList.GhostDifficultyData[(int)_missionDataHandler.MissionDifficulty], _playerCamera);
            
            return (Player)_netIdFactory.Initialize(player, NetId);
        }
    }
}