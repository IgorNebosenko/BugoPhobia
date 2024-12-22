using ElectrumGames.Core.Journal;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Lobby
{
    public class MissionResultHandler
    {
        public GhostType SelectedGhost { get; private set; } = GhostType.None;
        public GhostType CorrectGhost { get; private set; } = GhostType.None;

        private MissionsUnion _missionsUnion;
        
        private JournalManager _journalManager;

        public const int CoinsPerCorrectGhost = 100;
        public const int CoinsPerCorrectTask = 30;
        public const int CoinsPerIncorrectElement = 0;

        public int CountCoinsPerGhost => IsGhostCorrect ? CoinsPerCorrectGhost : CoinsPerIncorrectElement;

        public bool IsGhostCorrect => SelectedGhost == CorrectGhost;

        public MissionResultHandler(JournalManager journalManager)
        {
            _journalManager = journalManager;
        }

        public void OnLobbyEnter(GhostType correctType, MissionsUnion missionsUnion)
        {
            SelectedGhost = _journalManager.PlayerJournalInstance.SelectedGhost;
            CorrectGhost = correctType;

            _missionsUnion = missionsUnion;
            
            Debug.Log($"Is ghost type correct: {CorrectGhost == SelectedGhost}");
            Debug.Log($"First mission {_missionsUnion.FirstMissionType} passed {_missionsUnion.FirstMissionStatus}");
            Debug.Log($"Second mission {_missionsUnion.SecondMissionType} passed {_missionsUnion.SecondMissionStatus}");
            Debug.Log($"Third mission {_missionsUnion.ThirdMissionType} passed {_missionsUnion.ThirdMissionStatus}");
            
            _journalManager.PlayerJournalInstance.Reset();
            
            for (var i = 0; i < _journalManager.OtherPlayersJournalInstances.Count; i++)
                _journalManager.OtherPlayersJournalInstances[i].Reset();
        }
    }
}