using ElectrumGames.Core.Missions;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class RadiationGhostZone : MonoBehaviour
    {
        private EvidenceController _evidenceController;

        public void Init(EvidenceController evidenceController)
        {
            _evidenceController = evidenceController;
        }
    }
}