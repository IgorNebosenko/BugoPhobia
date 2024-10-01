using System;
using System.Collections.Generic;
using System.Linq;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using UnityEngine;

namespace ElectrumGames.Core.Ghost
{
    [Serializable]
    public class GhostModelData
    {
        [field: SerializeField] public GhostModelType GhostModelType { get; private set; }
        [field: SerializeField] public bool CanBeMale { get; private set; }
        [field: SerializeField] public bool CanBeFemale { get; private set; }
        [field: SerializeField] public GhostModelController GhostModelController { get; private set; }
    }

    [CreateAssetMenu(fileName = "GhostModelsList", menuName = "Ghosts configs/GhostModelsList")]
    public class GhostModelsList : ScriptableObject
    {
        [SerializeField] private GhostModelData[] ghostModelsData;

        public IReadOnlyList<GhostModelData> MaleModels => ghostModelsData.Where(x => x.CanBeMale).ToArray();
        
        public IReadOnlyList<GhostModelData> FemaleModels => ghostModelsData.Where(x => x.CanBeFemale).ToArray();
    }
}