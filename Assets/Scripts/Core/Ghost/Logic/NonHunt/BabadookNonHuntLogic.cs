﻿using System.Linq;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Ghost.Logic.NonHunt
{
    public class BabadookNonHuntLogic : BaseNonHuntLogic
    {
        private const float BabadookSelectedModifier = 0.75f;
        private const float ExcludeGhostModifier = 1.5f;
        private const float ExcludeEvidenceModifier = 1.25f;
        
        private readonly GhostController _ghostController;

        protected override float DoorsInteractions
        {
            get
            {
                if (_ghostController.JournalManager.PlayerJournalInstance.SelectedGhost == GhostType.Babadook ||
                    _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.SelectedGhost == GhostType.Babadook))
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.doorsInteractions * BabadookSelectedModifier;
                
                if (_ghostController.JournalManager.PlayerJournalInstance.DeselectedGhosts.Contains(GhostType.Babadook) ||
                    _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.DeselectedGhosts.Any(
                        y => y ==GhostType.Babadook)))
                return _ghostController.GhostEnvironmentHandler.GhostVariables.doorsInteractions * ExcludeGhostModifier;

                for (var i = 0; i < _ghostController.EvidenceController.Evidences.Count; i++)
                {
                    var evidence = _ghostController.EvidenceController.Evidences[i];
                    
                    if (_ghostController.JournalManager.PlayerJournalInstance.DeselectedEvidences.Contains(evidence) ||
                        _ghostController.JournalManager.OtherPlayersJournalInstances.Any(
                            y => y.DeselectedEvidences.Contains(evidence)))
                        return _ghostController.GhostEnvironmentHandler.GhostVariables.doorsInteractions *
                               ExcludeEvidenceModifier;
                }

                return _ghostController.GhostEnvironmentHandler.GhostVariables.doorsInteractions;
            }
        }
        protected override float SwitchesInteractions 
        {
            get
            {
                if (_ghostController.JournalManager.PlayerJournalInstance.SelectedGhost == GhostType.Babadook ||
                    _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.SelectedGhost == GhostType.Babadook))
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.switchesInteractions * BabadookSelectedModifier;
                
                if (_ghostController.JournalManager.PlayerJournalInstance.DeselectedGhosts.Contains(GhostType.Babadook) ||
                    _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.DeselectedGhosts.Any(
                        y => y ==GhostType.Babadook)))
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.switchesInteractions * ExcludeGhostModifier;

                for (var i = 0; i < _ghostController.EvidenceController.Evidences.Count; i++)
                {
                    var evidence = _ghostController.EvidenceController.Evidences[i];
                    
                    if (_ghostController.JournalManager.PlayerJournalInstance.DeselectedEvidences.Contains(evidence) ||
                        _ghostController.JournalManager.OtherPlayersJournalInstances.Any(
                            y => y.DeselectedEvidences.Contains(evidence)))
                        return _ghostController.GhostEnvironmentHandler.GhostVariables.switchesInteractions *
                               ExcludeEvidenceModifier;
                }

                return _ghostController.GhostEnvironmentHandler.GhostVariables.switchesInteractions;
            }
        }
        protected override float ThrowsInteractions
        {
            get
            {
                if (_ghostController.JournalManager.PlayerJournalInstance.SelectedGhost == GhostType.Babadook ||
                    _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.SelectedGhost == GhostType.Babadook))
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.throws * BabadookSelectedModifier;
                
                if (_ghostController.JournalManager.PlayerJournalInstance.DeselectedGhosts.Contains(GhostType.Babadook) ||
                    _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.DeselectedGhosts.Any(
                        y => y ==GhostType.Babadook)))
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.throws * ExcludeGhostModifier;

                for (var i = 0; i < _ghostController.EvidenceController.Evidences.Count; i++)
                {
                    var evidence = _ghostController.EvidenceController.Evidences[i];
                    
                    if (_ghostController.JournalManager.PlayerJournalInstance.DeselectedEvidences.Contains(evidence) ||
                        _ghostController.JournalManager.OtherPlayersJournalInstances.Any(
                            y => y.DeselectedEvidences.Contains(evidence)))
                        return _ghostController.GhostEnvironmentHandler.GhostVariables.throws *
                               ExcludeEvidenceModifier;
                }

                return _ghostController.GhostEnvironmentHandler.GhostVariables.throws;
            }
        }
        protected override float OtherInteractions
        {
            get
            {
                if (_ghostController.JournalManager.PlayerJournalInstance.SelectedGhost == GhostType.Babadook ||
                    _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.SelectedGhost == GhostType.Babadook))
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.otherInteractions * BabadookSelectedModifier;
                
                if (_ghostController.JournalManager.PlayerJournalInstance.DeselectedGhosts.Contains(GhostType.Babadook) ||
                    _ghostController.JournalManager.OtherPlayersJournalInstances.Any(x => x.DeselectedGhosts.Any(
                        y => y ==GhostType.Babadook)))
                    return _ghostController.GhostEnvironmentHandler.GhostVariables.otherInteractions * ExcludeGhostModifier;

                for (var i = 0; i < _ghostController.EvidenceController.Evidences.Count; i++)
                {
                    var evidence = _ghostController.EvidenceController.Evidences[i];
                    
                    if (_ghostController.JournalManager.PlayerJournalInstance.DeselectedEvidences.Contains(evidence) ||
                        _ghostController.JournalManager.OtherPlayersJournalInstances.Any(
                            y => y.DeselectedEvidences.Contains(evidence)))
                        return _ghostController.GhostEnvironmentHandler.GhostVariables.otherInteractions *
                               ExcludeEvidenceModifier;
                }

                return _ghostController.GhostEnvironmentHandler.GhostVariables.otherInteractions;
            }
        }

        public BabadookNonHuntLogic(GhostController ghostController, GhostDifficultyData ghostDifficultyData,
            GhostActivityData activityData, GhostEmfZonePool emfZonesPool, EmfData emfData) : base(ghostController,
            ghostDifficultyData, activityData, emfZonesPool, emfData)
        {
            _ghostController = ghostController;
        }
    }
}