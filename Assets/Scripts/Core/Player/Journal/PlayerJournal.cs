using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Player.Journal
{
    public class PlayerJournal : IHaveJournal
    {
        public event Action<EvidenceType> EvidenceClicked;
        public event Action<GhostType> GhostClicked;
        
        public List<EvidenceType> SelectedEvidences { get; private set; } = new();
        public List<EvidenceType> DeselectedEvidences { get; private set; } = new();

        public GhostType SelectedGhost { get; private set; } = GhostType.None;
        public List<GhostType> DeselectedGhosts { get; private set; } = new();

        public void Reset()
        {
            SelectedEvidences.Clear();
            DeselectedEvidences.Clear();
            
            SelectedGhost = GhostType.None;
            DeselectedGhosts.Clear();
        }

        public void OnEvidenceClicked(EvidenceType evidence)
        {
            EvidenceClicked?.Invoke(evidence);
            if (SelectedEvidences.Contains(evidence))
            {
                SelectedEvidences.Remove(evidence);
                DeselectedEvidences.Add(evidence);
            }

            if (DeselectedEvidences.Contains(evidence))
            {
                DeselectedEvidences.Remove(evidence);
            }
        }

        public void OnGhostClicked(GhostType ghost)
        {
            GhostClicked?.Invoke(ghost);

            if (DeselectedGhosts.Contains(ghost))
            {
                DeselectedGhosts.Remove(ghost);
            }
            else
            {
                SelectedGhost = ghost;
            }
        }
    }
}