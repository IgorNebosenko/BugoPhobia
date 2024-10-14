using System.Collections.Generic;
using System.Linq;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Player
{
    public class MissionPlayersHandler
    {
        private readonly GhostDifficultyList _difficultyList;
        private readonly MissionDataHandler _missionDataHandler;
        
        private List<IPlayer> _players = new();

        public float AverageSanity => _players.Average(x => x.Sanity.CurrentSanity);
        public bool IsAnyPlayerInHouse => _players.Any(x => x.GetCurrentStayRoom() != -1);
        public bool IsAnyPlayerAlive => _players.Any(x => x.IsAlive);

        public MissionPlayersHandler(GhostDifficultyList difficultyList, MissionDataHandler missionDataHandler)
        {
            _difficultyList = difficultyList;
            _missionDataHandler = missionDataHandler;
        }

        public void ConnectPlayer(IPlayer player)
        {
            _players.Add(player);
        }

        public void DisconnectPlayer(IPlayer player)
        {
            _players.Remove(player);
            
            for (var i = 0; i < _players.Count; i++)
            {
                _players[i].Sanity.ChangeSanity(
                    _difficultyList.GhostDifficultyData[(int)_missionDataHandler.MissionDifficulty].
                        GhostEventDrainOnKillOrDisconnectMate, player.NetId);
            }
        }
    }
}