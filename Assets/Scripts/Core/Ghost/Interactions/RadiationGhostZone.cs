using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Missions;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class RadiationGhostZone : MonoBehaviour
    {
        private EvidenceController _evidenceController;
        private RadiationConfig _radiationConfig;

        public void Init(EvidenceController evidenceController, RadiationConfig radiationConfig)
        {
            _evidenceController = evidenceController;
            _radiationConfig = radiationConfig;
        }
    }
}