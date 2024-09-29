using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Configs
{
    [Serializable]
    public class GhostDifficultyData
    {
        [field: SerializeField] public GameDifficulty GameDifficulty { get; private set; }
        [field: SerializeField] public float ThrowCooldown { get; private set; }
        [field: SerializeField] public float DoorsInteractionCooldown { get; private set; }
        [field: SerializeField] public float SwitchesInteractionCooldown { get; private set; }
        [field: SerializeField] public float OtherInteractionCooldown { get; private set; }
        [field: Space]
        
        [field: SerializeField] public float MovingToPointCooldown { get; private set; }
        [field: SerializeField] public float MovingToPointChance { get; private set; }
        [field: Space]
        [field: SerializeField] public float GhostEventsCooldownModifier { get; private set; }
        [field: SerializeField] public float MobGhostEventsCooldownModifier { get; private set; }
        [field: Space]
        [field: SerializeField] public float InRoomWeightPoint { get; private set; }
        [field: SerializeField] public float NeighborRoomWeightPoint { get; private set; }
        [field: SerializeField] public float OtherWeightPoint { get; private set; }
        
    }

    [CreateAssetMenu(fileName = "GhostDifficultyList", menuName = "Ghosts configs/Ghost difficulty list")]
    public class GhostDifficultyList : ScriptableObject
    {
        [SerializeField] private GhostDifficultyData[] ghostDifficultyData;

        public IReadOnlyList<GhostDifficultyData> GhostDifficultyData => ghostDifficultyData;
    }
}