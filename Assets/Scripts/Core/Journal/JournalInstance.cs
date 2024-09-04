using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Journal
{
    public enum JournalItemState
    {
        Unselected,
        Selected,
        Deselected
    }

    public class JournalInstance
    {
        public HashSet<EvidenceType> SelectedEvidences { get; private set; }
        public HashSet<EvidenceType> DeselectedEvidences { get; private set; }

        public GhostType SelectedGhost { get; set; }
        public HashSet<GhostType> DeselectedGhosts { get; private set; }
        
        public event Action JournalUpdated;

        public JournalInstance()
        {
            SelectedEvidences = new HashSet<EvidenceType>();
            DeselectedEvidences = new HashSet<EvidenceType>();
            
            SelectedGhost = GhostType.None;
            DeselectedGhosts = new HashSet<GhostType>();
        }

        public void HandleEvidence(EvidenceType evidence, JournalItemState previousState)
        {
            switch (previousState)
            {
                case JournalItemState.Unselected:
                    SelectedEvidences.Add(evidence);
                    DeselectedEvidences.Remove(evidence);
                    break;
                case JournalItemState.Selected:
                    SelectedEvidences.Remove(evidence);
                    DeselectedEvidences.Add(evidence);
                    break;
                case JournalItemState.Deselected:
                    SelectedEvidences.Remove(evidence);
                    DeselectedEvidences.Remove(evidence);
                    break;
            }
            
            JournalUpdated?.Invoke();
        }
        
        public JournalItemState GetEvidenceState(EvidenceType evidenceType)
        {
            if (SelectedEvidences.Contains(evidenceType))
            {
                return JournalItemState.Selected;
            }

            if (DeselectedEvidences.Contains(evidenceType))
            {
                return JournalItemState.Deselected;
            }
            
            return JournalItemState.Unselected;
        }

        public void HandleGhost(GhostType ghost, JournalItemState previousState)
        {
            switch (previousState)
            {
                case JournalItemState.Unselected:
                    SelectedGhost = ghost;
                    DeselectedGhosts.Remove(ghost);
                    break;
                case JournalItemState.Selected:
                    SelectedGhost = GhostType.None;
                    DeselectedGhosts.Add(ghost);
                    break;
                case JournalItemState.Deselected:
                    SelectedGhost = GhostType.None;
                    DeselectedGhosts.Remove(ghost);
                    break;
            }
            
            JournalUpdated?.Invoke();
        }
        
        public JournalItemState GetUserGhostState(GhostType ghostType)
        {
            if (SelectedGhost == ghostType)
            {
                return JournalItemState.Selected;
            }

            if (DeselectedGhosts.Contains(ghostType))
            {
                return JournalItemState.Deselected;
            }
            
            return JournalItemState.Unselected;
        }

        public void Reset()
        {
            SelectedEvidences.Clear();
            DeselectedEvidences.Clear();
            
            SelectedGhost = GhostType.None;
            DeselectedGhosts.Clear();
        }
    }
}