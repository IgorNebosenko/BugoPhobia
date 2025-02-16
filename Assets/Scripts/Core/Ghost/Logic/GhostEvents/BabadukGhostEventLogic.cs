using System.Linq;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Player;
using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Ghost.Logic.GhostEvents
{
    public class BabadukGhostEventLogic : BaseGhostEvent
    {
        private const float BabadukSelectedModifier = 0.75f;
        private const float ExcludeGhostModifier = 1.5f;
        private const float ExcludeEvidenceModifier = 1.25f;
        
        private readonly GhostController _ghostController;
        
        protected override float GhostEventsFrequency
        {
            get
            {
                if (_ghostController.JournalManager.PlayerJournalInstance.SelectedGhost == GhostType.Babaduk ||
                    _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.SelectedGhost == GhostType.Babaduk))
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.ghostEvents * BabadukSelectedModifier;
                
                if (_ghostController.JournalManager.PlayerJournalInstance.DeselectedGhosts.Contains(GhostType.Babaduk) ||
                    _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.DeselectedGhosts.Any(
                        y => y ==GhostType.Babaduk)))
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.ghostEvents * ExcludeGhostModifier;

                for (var i = 0; i < _ghostController.EvidenceController.Evidences.Count; i++)
                {
                    var evidence = _ghostController.EvidenceController.Evidences[i];
                    
                    if (_ghostController.JournalManager.PlayerJournalInstance.DeselectedEvidences.Contains(evidence) ||
                        _ghostController.JournalManager.OtherPlayersJournalInstances.Any(
                            y => y.DeselectedEvidences.Contains(evidence)))
                        return _ghostController.GhostEnvironmentHandler.GhostVariables.ghostEvents *
                               ExcludeEvidenceModifier;
                }

                return _ghostController.GhostEnvironmentHandler.GhostVariables.ghostEvents;
            }
        }
        
        public BabadukGhostEventLogic(GhostController ghostController, GhostDifficultyData difficultyData,
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData, 
            MissionPlayersHandler missionPlayersHandler) : base(ghostController, difficultyData, activityData,
            emfZonesPool, emfData, missionPlayersHandler)
        {
            _ghostController = ghostController;
        }
    }
}