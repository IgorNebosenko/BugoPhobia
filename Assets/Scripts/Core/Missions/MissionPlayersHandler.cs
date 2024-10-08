using System.Collections.Generic;
using System.Linq;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Player;
using UnityEngine;

namespace ElectrumGames.Core.Missions
{
    public class MissionPlayersHandler
    {
        private readonly GhostDifficultyList _difficultyList;
        
        private List<IPlayer> _players = new();

        public float AverageSanity => _players.Average(x => x.Sanity.CurrentSanity);

        public MissionPlayersHandler(GhostDifficultyList difficultyList)
        {
            _difficultyList = difficultyList;
        }

        public void ConnectPlayer(IPlayer player)
        {
            _players.Add(player);
        }

        public void DisconnectPlayer(IPlayer player)
        {
            _players.Remove(player);

            Debug.LogWarning("Set difficulty from list by config!");
            for (var i = 0; i < _players.Count; i++)
            {
                _players[i].Sanity.ChangeSanity(
                    _difficultyList.GhostDifficultyData[0].GhostEventDrainOnKillOrDisconnectMate, player.NetId);
            }
        }
    }
}