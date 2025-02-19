using System.Collections.Generic;
using ElectrumGames.Core.Journal;
using ElectrumGames.Extensions;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Logic.Abilities
{
    public class ImpAbility : IGhostAbility
    {
        private readonly IReadOnlyList<JournalInstance> _journals;

        private const int CountEvidences = (int)EvidenceType.SpiritBox + 1;
        private const int CountGhosts = (int)GhostType.Lich + 1;

        private GhostConstants _ghostConstants;
        
        private float _cooldownTime;
        
        public bool IsInterrupt { get; set; }

        public ImpAbility(JournalManager journalManager)
        {
            var journals = journalManager.OtherPlayersJournalInstances;
            journals.Add(journalManager.PlayerJournalInstance);
            journals.Shuffle();

            _journals = journals;
        }
        
        public void Setup(GhostVariables variables, GhostConstants constants, int roomId)
        {
            _ghostConstants = constants;
        }

        public void FixedSimulate()
        {
            if (IsInterrupt)
                return;

            _cooldownTime += Time.fixedDeltaTime;
            
            if (_cooldownTime >= _ghostConstants.abilityCooldown)
            {
                _cooldownTime = 0f;

                if (Random.Range(0f, 1f) < _ghostConstants.abilityChance)
                {
                    TryUseAbility();
                }
            }
        }

        public bool TryUseAbility()
        {
            var journal = _journals.PickRandom();

            if (Random.Range(0f, 1f) < 0.5f)
            {
                if (TryDeselectAt(journal))
                    TrySelectAt(journal);
            }
            else
            {
                TryDeselectAt(journal);
            }

            return false;
        }

        private bool TryDeselectAt(JournalInstance journalInstance)
        {
            if (journalInstance.SelectedGhost == GhostType.None &&
                journalInstance.DeselectedEvidences.Count == CountEvidences &&
                journalInstance.DeselectedGhosts.Count == CountGhosts)
                return false;

            if (Random.Range(0f, 1f) <= 0.5f) //50% for deselect evidence
            {
                var setEvidences = new HashSet<EvidenceType>();

                for (var i = 0; i < CountEvidences; i++)
                {
                    if (!journalInstance.DeselectedEvidences.Contains((EvidenceType) i))
                        setEvidences.Add((EvidenceType) i);
                }

                var deselectedEvidence = setEvidences.PickRandom();
                
                Debug.Log($"Ghost deselect at journal evidence {deselectedEvidence}");
                journalInstance.DeselectedEvidences.Add(deselectedEvidence);
            }
            else // 50% for deselect ghost
            {
                var setGhosts = new HashSet<GhostType>();

                for (var i = 0; i < CountGhosts; i++)
                {
                    if (!journalInstance.DeselectedGhosts.Contains((GhostType) i))
                        setGhosts.Add((GhostType) i);
                }

                var deselectedGhost = setGhosts.PickRandom();
                
                Debug.Log($"Ghost deselect at journal ghost {deselectedGhost}");
                journalInstance.DeselectedGhosts.Add(deselectedGhost);

                if (journalInstance.SelectedGhost == deselectedGhost)
                    journalInstance.SelectedGhost = GhostType.None;
            }

            return true;
        }

        private void TrySelectAt(JournalInstance journalInstance)
        {
            if (Random.Range(0f, 1f) <= 0.5f) // 50% select evidence
            {
                var setEvidences = new HashSet<EvidenceType>();
                
                for (var i = 0; i < CountEvidences; i++)
                {
                    if (!journalInstance.SelectedEvidences.Contains((EvidenceType) i))
                        setEvidences.Add((EvidenceType) i);
                }

                var selectedEvidence = setEvidences.PickRandom();
                
                Debug.Log($"Ghost select at journal evidence {selectedEvidence}");
                journalInstance.SelectedEvidences.Add(selectedEvidence);
            }
            else //50% select ghost
            {
                var setGhosts = new HashSet<GhostType>();

                for (var i = 0; i < CountGhosts; i++)
                {
                    if (journalInstance.SelectedGhost != (GhostType) i)
                        setGhosts.Add((GhostType) i);
                }

                var selectedGhost = setGhosts.PickRandom();
                
                Debug.Log($"Ghost select at journal ghost {selectedGhost}");
                journalInstance.SelectedGhost = selectedGhost;
            }
        }
    }
}