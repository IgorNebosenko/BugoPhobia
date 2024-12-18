using ElectrumGames.Core.Journal;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Lobby
{
    public class MissionResultHandler
    {
        private GhostType _selectedGhost = GhostType.None;
        private GhostType _correctGhost = GhostType.None;

        private MissionsUnion _missionsUnion;
        
        private JournalManager _journalManager;

        public MissionResultHandler(JournalManager journalManager)
        {
            _journalManager = journalManager;
        }

        public void OnLobbyEnter(GhostType correctType, MissionsUnion missionsUnion)
        {
            _selectedGhost = _journalManager.PlayerJournalInstance.SelectedGhost;
            _correctGhost = correctType;

            _missionsUnion = missionsUnion;
            
            Debug.Log($"Is ghost type correct: {_correctGhost == _selectedGhost}");
            Debug.Log($"First mission {_missionsUnion.FirstMissionType} passed {_missionsUnion.FirstMissionStatus}");
            Debug.Log($"Second mission {_missionsUnion.SecondMissionType} passed {_missionsUnion.SecondMissionStatus}");
            Debug.Log($"Third mission {_missionsUnion.ThirdMissionType} passed {_missionsUnion.ThirdMissionStatus}");
            
            _journalManager.PlayerJournalInstance.Reset();
            
            for (var i = 0; i < _journalManager.OtherPlayersJournalInstances.Count; i++)
                _journalManager.OtherPlayersJournalInstances[i].Reset();
        }
    }
}