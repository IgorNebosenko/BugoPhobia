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

        public GhostType SelectedGhost { get; private set; }
        public HashSet<GhostType> DeselectedGhosts { get; private set; }
        
        public event Action JournalUpdated;

        public JournalInstance()
        {
            SelectedEvidences = new HashSet<EvidenceType>();
            DeselectedEvidences = new HashSet<EvidenceType>();
            
            SelectedGhost = GhostType.None;
            DeselectedGhosts = new HashSet<GhostType>();
        }

        public void HandleEvidence(EvidenceType evidence, JournalItemState state)
        {
            switch (state)
            {
                case JournalItemState.Unselected:
                    SelectedEvidences.Remove(evidence);
                    DeselectedEvidences.Remove(evidence);
                    break;
                case JournalItemState.Selected:
                    SelectedEvidences.Add(evidence);
                    DeselectedEvidences.Remove(evidence);
                    break;
                case JournalItemState.Deselected:
                    SelectedEvidences.Remove(evidence);
                    DeselectedEvidences.Add(evidence);
                    break;
            }
            
            JournalUpdated?.Invoke();
        }

        public void HandleGhost(GhostType ghost, JournalItemState state)
        {
            switch (state)
            {
                case JournalItemState.Unselected:
                    SelectedGhost = GhostType.None;
                    DeselectedGhosts.Remove(ghost);
                    break;
                case JournalItemState.Selected:
                    SelectedGhost = ghost;
                    DeselectedGhosts.Remove(ghost);
                    break;
                case JournalItemState.Deselected:
                    SelectedGhost = GhostType.None;
                    DeselectedGhosts.Add(ghost);
                    break;
            }
            
            JournalUpdated?.Invoke();
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