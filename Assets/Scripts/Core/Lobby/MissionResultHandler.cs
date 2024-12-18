using ElectrumGames.Core.Journal;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Lobby
{
    public class MissionResultHandler
    {
        private GhostType _selectedGhost = GhostType.None;
        
        private JournalManager _journalManager;

        public MissionResultHandler(JournalManager journalManager)
        {
            _journalManager = journalManager;
        }

        public void OnLobbyEnter(GhostType correctType, bool isFirstMissionPassed, bool isSecondMissionPassed,
            bool isThirdMissionPassed)
        {
            _selectedGhost = _journalManager.PlayerJournalInstance.SelectedGhost;
            
            Debug.Log($"Is ghost type correct: {correctType == _selectedGhost}");
            Debug.Log($"First mission passed {isFirstMissionPassed}");
            Debug.Log($"Second mission passed {isSecondMissionPassed}");
            Debug.Log($"Third mission passed {isThirdMissionPassed}");
            
            _journalManager.PlayerJournalInstance.Reset();
            
            for (var i = 0; i < _journalManager.OtherPlayersJournalInstances.Count; i++)
                _journalManager.OtherPlayersJournalInstances[i].Reset();
        }
    }
}