using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Player.Journal
{
    public interface IHaveJournal
    {
        event Action<EvidenceType> EvidenceClicked;
        event Action<GhostType> GhostClicked;
        
        List<EvidenceType> SelectedEvidences { get; }
        List<EvidenceType> DeselectedEvidences { get; }
        
        GhostType SelectedGhost { get; }
        List<GhostType> DeselectedGhosts { get; }

        void Reset();
        void OnEvidenceClicked(EvidenceType evidence);
        void OnGhostClicked(GhostType ghost);
    }
}
