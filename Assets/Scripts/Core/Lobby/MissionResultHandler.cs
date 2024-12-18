using ElectrumGames.Core.Journal;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Lobby
{
    public class MissionResultHandler
    {
        private GhostType _selectedGhost = GhostType.None;
        private GhostType _correctGhost = GhostType.None;

        private bool _isFirstMissionPassed;
        private bool _isSecondMissionPassed;
        private bool _isThirdMissionPassed;
        
        private JournalManager _journalManager;

        public MissionResultHandler(JournalManager journalManager)
        {
            _journalManager = journalManager;
        }

        public void OnLobbyEnter(GhostType correctType, bool isFirstMissionPassed, bool isSecondMissionPassed,
            bool isThirdMissionPassed)
        {
            _selectedGhost = _journalManager.PlayerJournalInstance.SelectedGhost;
            _correctGhost = correctType;

            _isFirstMissionPassed = isFirstMissionPassed;
            _isSecondMissionPassed = isSecondMissionPassed;
            _isThirdMissionPassed = isThirdMissionPassed;
            
            Debug.Log($"Is ghost type correct: {_correctGhost == _selectedGhost}");
            Debug.Log($"First mission passed {_isFirstMissionPassed}");
            Debug.Log($"Second mission passed {_isSecondMissionPassed}");
            Debug.Log($"Third mission passed {_isThirdMissionPassed}");
            
            _journalManager.PlayerJournalInstance.Reset();
            
            for (var i = 0; i < _journalManager.OtherPlayersJournalInstances.Count; i++)
                _journalManager.OtherPlayersJournalInstances[i].Reset();
        }
    }
}