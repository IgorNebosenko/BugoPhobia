using System;
using ElectrumGames.GlobalEnums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ElectrumGames.Configs
{
    [Serializable]
    public class DescriptionConfigData
    {
        [field: SerializeField] public GhostType GhostType { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: SerializeField, TextArea] public string Strength { get; private set; }
        [field: SerializeField, TextArea] public string Weaknesses { get; private set; }
    }
    
    

    [CreateAssetMenu(fileName = "Description Config", menuName = "Ghosts configs/Description Config")]
    public class DescriptionConfig : ScriptableObject
    {
        [field: SerializeField, ListDrawerSettings(NumberOfItemsPerPage = 3)] 
        public DescriptionConfigData[] Data { get; private set; }
    }
}