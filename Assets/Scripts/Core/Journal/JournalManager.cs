using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Journal
{
    public class JournalManager
    {
        public JournalInstance PlayerJournalInstance { get; private set; }
        
        public List<JournalInstance> OtherPlayersJournalInstances { get; private set; }

        public JournalManager()
        {
            PlayerJournalInstance = new JournalInstance();
            OtherPlayersJournalInstances = new List<JournalInstance>();
        }
        
        public JournalItemState GetUserEvidenceState(EvidenceType evidenceType)
        {
            if (PlayerJournalInstance.SelectedEvidences.Contains(evidenceType))
            {
                return JournalItemState.Selected;
            }

            if (PlayerJournalInstance.DeselectedEvidences.Contains(evidenceType))
            {
                return JournalItemState.Deselected;
            }
            
            return JournalItemState.Unselected;
        }

        public void SetUserEvidenceState(EvidenceType evidence, JournalItemState previousState)
        {
            switch (previousState)
            {
                case JournalItemState.Unselected:
                    PlayerJournalInstance.SelectedEvidences.Add(evidence);
                    PlayerJournalInstance.DeselectedEvidences.Remove(evidence);
                    break;
                case JournalItemState.Selected:
                    PlayerJournalInstance.SelectedEvidences.Remove(evidence);
                    PlayerJournalInstance.DeselectedEvidences.Add(evidence);
                    break;
                case JournalItemState.Deselected:
                    PlayerJournalInstance.SelectedEvidences.Remove(evidence);
                    PlayerJournalInstance.DeselectedEvidences.Remove(evidence);
                    break;
            }
        }

        public JournalItemState GetUserGhostState(GhostType ghostType)
        {
            if (PlayerJournalInstance.SelectedGhost == ghostType)
            {
                return JournalItemState.Selected;
            }

            if (PlayerJournalInstance.DeselectedGhosts.Contains(ghostType))
            {
                return JournalItemState.Deselected;
            }
            
            return JournalItemState.Unselected;
        }

        public void SetUserGhost(GhostType ghost, JournalItemState previousState)
        {
            switch (previousState)
            {
                case JournalItemState.Unselected:
                    PlayerJournalInstance.SelectedGhost = ghost;
                    PlayerJournalInstance.DeselectedGhosts.Remove(ghost);
                    break;
                case JournalItemState.Selected:
                    PlayerJournalInstance.SelectedGhost = GhostType.None;
                    PlayerJournalInstance.DeselectedGhosts.Add(ghost);
                    break;
                case JournalItemState.Deselected:
                    PlayerJournalInstance.SelectedGhost = GhostType.None;
                    PlayerJournalInstance.DeselectedGhosts.Remove(ghost);
                    break;
            }
        }
    }
}
