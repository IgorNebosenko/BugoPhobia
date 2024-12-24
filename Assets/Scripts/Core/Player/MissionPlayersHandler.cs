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
        
        public List<IPlayer> Players { get; private set; }= new();

        public float AverageSanity => Players.Average(x => x.Sanity.CurrentSanity);
        public bool IsAnyPlayerInHouse => Players.Any(x => x.GetCurrentStayRoom() != -1);
        public bool IsAnyPlayerAlive => Players.Any(x => x.IsAlive);

        public MissionPlayersHandler(GhostDifficultyList difficultyList, MissionDataHandler missionDataHandler)
        {
            _difficultyList = difficultyList;
            _missionDataHandler = missionDataHandler;
        }

        public void ConnectPlayer(IPlayer player)
        {
            Players.Add(player);
        }

        public void DisconnectPlayer(IPlayer player)
        {
            Players.Remove(player);
            
            for (var i = 0; i < Players.Count; i++)
            {
                Players[i].Sanity.ChangeSanity(
                    _difficultyList.GhostDifficultyData[(int)_missionDataHandler.MissionDifficulty].
                        GhostEventDrainOnKillOrDisconnectMate, player.NetId);
            }
        }
    }
}