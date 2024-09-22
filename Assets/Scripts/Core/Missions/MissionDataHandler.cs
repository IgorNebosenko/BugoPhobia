using System.Collections.Generic;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Lobby;

namespace ElectrumGames.Core.Missions
{
    public class MissionDataHandler
    {
        private readonly LobbyItemsHandler _lobbyItemsHandler;

        public IReadOnlyList<ItemLobbyData> LobbyItemsData => _lobbyItemsHandler.GetSortedList();
        public MissionDifficulty MissionDifficulty { get; set; } = MissionDifficulty.Medium;
        public MissionMap MissionMap { get; set; } = MissionMap.SmallHouse;
        public int PlayerJournalId { get; set; }

        public bool CheckCanStart()
        {
            return MissionDifficulty != MissionDifficulty.None &&
                   MissionMap != MissionMap.None &&
                   PlayerJournalId > 0;
        }
    }
}
