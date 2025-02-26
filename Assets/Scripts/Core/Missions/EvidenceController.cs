using System.Collections.Generic;
using System.Linq;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Missions
{
    public class EvidenceController
    {
        private EmfData _emfData;
        public IReadOnlyList<EvidenceType> Evidences { get; private set; }
        
        public EvidenceController(EmfData emfData)
        {
            _emfData = emfData;
            Debug.LogWarning("There's set explicit evidences!");
            SetData(new [] {EvidenceType.EMF5, EvidenceType.UV, EvidenceType.FreezingTemperature});
        }

        public void SetData(IReadOnlyList<EvidenceType> evidences)
        {
            Evidences = evidences;
        }

        public int GetEmfInteractDoor()
        {
            if (Random.Range(0f, 1f) > _emfData.ChanceEvidence || !Evidences.Contains(EvidenceType.EMF5))
                return _emfData.DoorDefaultEmf;

            return _emfData.EvidenceLevel;
        }

        public int GetEmfInteractSwitch()
        {
            if (Random.Range(0f, 1f) > _emfData.ChanceEvidence || !Evidences.Contains(EvidenceType.EMF5))
                return _emfData.SwitchDefaultEmf;

            return _emfData.EvidenceLevel;
        }

        public int OnThrowInteract()
        {
            if (Random.Range(0f, 1f) > _emfData.ChanceEvidence || !Evidences.Contains(EvidenceType.EMF5))
                return _emfData.ThrowDefaultEmf;

            return _emfData.EvidenceLevel;
        }
        
        public int GetEmfOtherInteract()
        {
            if (Random.Range(0f, 1f) > _emfData.ChanceEvidence || !Evidences.Contains(EvidenceType.EMF5))
                return _emfData.OtherInteractionDefaultEmf;

            return _emfData.EvidenceLevel;
        }
    }
}