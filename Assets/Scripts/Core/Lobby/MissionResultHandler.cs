using ElectrumGames.Core.Journal;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Lobby
{
    public class MissionResultHandler
    {
        public GhostType SelectedGhost { get; private set; } = GhostType.None;
        public GhostType CorrectGhost { get; private set; } = GhostType.None;

        public MissionsUnion MissionsUnion { get; private set; }
        
        private JournalManager _journalManager;
        private MoneysHandler _moneysHandler;

        public const int CoinsPerCorrectGhost = 100;
        public const int CoinsPerCorrectTask = 30;
        public const int CoinsPerIncorrectElement = 0;

        public int CountCoinsPerGhost => IsGhostCorrect ? CoinsPerCorrectGhost : CoinsPerIncorrectElement;

        public bool IsGhostCorrect => SelectedGhost == CorrectGhost;
        public int TotalCoins => IsGhostCorrect ? CoinsPerCorrectGhost : CoinsPerIncorrectElement +
                   (MissionsUnion.FirstMissionStatus ? CoinsPerCorrectGhost : CoinsPerIncorrectElement) +
                   (MissionsUnion.SecondMissionStatus ? CoinsPerCorrectGhost : CoinsPerIncorrectElement) +
                   (MissionsUnion.ThirdMissionStatus ? CoinsPerCorrectGhost : CoinsPerIncorrectElement);

        public MissionResultHandler(JournalManager journalManager, MoneysHandler moneysHandler)
        {
            _journalManager = journalManager;
            _moneysHandler = moneysHandler;
        }

        public void OnLobbyEnter(GhostType correctType, MissionsUnion missionsUnion)
        {
            SelectedGhost = _journalManager.PlayerJournalInstance.SelectedGhost;
            CorrectGhost = correctType;

            MissionsUnion = missionsUnion;
            
            Debug.Log($"Is ghost type correct: {CorrectGhost == SelectedGhost}");
            Debug.Log($"First mission {MissionsUnion.FirstMissionType} passed {MissionsUnion.FirstMissionStatus}");
            Debug.Log($"Second mission {MissionsUnion.SecondMissionType} passed {MissionsUnion.SecondMissionStatus}");
            Debug.Log($"Third mission {MissionsUnion.ThirdMissionType} passed {MissionsUnion.ThirdMissionStatus}");
            
            _journalManager.PlayerJournalInstance.Reset();
            
            for (var i = 0; i < _journalManager.OtherPlayersJournalInstances.Count; i++)
                _journalManager.OtherPlayersJournalInstances[i].Reset();

            _moneysHandler.Moneys += TotalCoins;
        }
    }
}