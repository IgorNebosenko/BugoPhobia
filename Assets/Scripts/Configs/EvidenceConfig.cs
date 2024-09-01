using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ElectrumGames.Configs
{
    [Serializable]
    public class EvidenceConfigData
    {
        [field: SerializeField] public GhostType GhostType { get; private set; }
        [field: SerializeField] public EvidenceType[] Evidences {get; private set;}
        [field: SerializeField] public bool IsFirstMandatory { get; private set; }
    }

    [CreateAssetMenu(fileName = "EvidenceConfig", menuName = "Ghosts configs/Evidences")]
    public class EvidenceConfig : ScriptableObject
    {
        [ListDrawerSettings(NumberOfItemsPerPage = 3)] 
        [SerializeField]
        private List<EvidenceConfigData> configData;
        
        public IReadOnlyList<EvidenceConfigData> ConfigData => configData;
    }
}