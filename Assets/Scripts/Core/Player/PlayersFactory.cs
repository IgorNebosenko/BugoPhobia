using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
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

        private Camera _playerCamera;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }
        
        [Inject]
        private void Construct(NetIdFactory netIdFactory, PlayerConfig playerConfig, InputActions inputActions,
            ConfigService configService, Camera playerCamera)
        {
            _netIdFactory = netIdFactory;
            _playerConfig = playerConfig;
            _inputActions = inputActions;
            _configService = configService;

            _playerCamera = playerCamera;
            
            SetNetId(_netIdFactory.GetNextNetId());
        }
        
        public IPlayer CreatePlayer(bool isHost, Vector3 position, Quaternion rotation)
        {
            var player = Instantiate(playerTemplate, transform);
            player.Spawn(_playerConfig, _configService, isHost, _inputActions, _playerCamera);
            
            player.transform.position = position;
            player.transform.rotation = rotation;
            
            return (Player)_netIdFactory.Initialize(player, NetId);
        }
    }
}