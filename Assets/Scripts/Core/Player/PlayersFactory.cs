using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Core.Environment;
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

        private DiContainer _container;
        
        private NetIdFactory _netIdFactory;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }
        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }
        
        [Inject]
        private void Construct(DiContainer container, NetIdFactory netIdFactory)
        {
            _container = container;
            
            _netIdFactory = netIdFactory;

            _netIdFactory.Initialize(this);
        }
        
        public IPlayer CreatePlayer(bool isPlayablePlayer, bool isHost, Vector3 position, Quaternion rotation)
        {
            var player = Instantiate(playerTemplate, position, rotation, transform);

            var difficultyList = _container.Resolve<GhostDifficultyList>();
            var missionDataHandler = _container.Resolve<MissionDataHandler>();
            
            player.Spawn(_container, difficultyList.GhostDifficultyData[(int)missionDataHandler.MissionDifficulty],
                isPlayablePlayer, isHost);
            
            return (Player)_netIdFactory.Initialize(player, NetId);
        }
    }
}