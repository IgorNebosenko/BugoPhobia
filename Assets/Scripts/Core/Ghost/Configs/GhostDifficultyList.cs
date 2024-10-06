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
        [field: SerializeField, Range(0f, 1f)] public float MovingToPointChance { get; private set; }
        [field: Space]
        [field: SerializeField] public float GhostEventsCooldownModifier { get; private set; }
        [field: SerializeField] public float MinStayGhostEventTime { get; private set; } = 3f;
        [field: SerializeField] public float MaxStayGhostEventTime { get; private set; } = 4f;
        [field: SerializeField] public float RedLightChance { get; private set; } = 0.2f;
        [field: Space]
        [field: SerializeField] public float MinChaseGhostEventTime { get; private set; } = 6f;
        [field: SerializeField] public float MaxChaseGhostEventTime { get; private set; } = 7f;
        [field: Space]
        [field: SerializeField] public float MinSingingGhostEventTime { get; private set; } = 8f;
        [field: SerializeField] public float MaxSingingGhostEventTime { get; private set; } = 11f;
        [field: Space]
        [field: SerializeField] public float InRoomWeightPoint { get; private set; }
        [field: SerializeField] public float NeighborRoomWeightPoint { get; private set; }
        [field: SerializeField] public float OtherWeightPoint { get; private set; }
        [field: Space]
        [field: SerializeField] public float SafeHuntTime { get; private set; } = 3f;
    }

    [CreateAssetMenu(fileName = "GhostDifficultyList", menuName = "Ghosts configs/Ghost difficulty list")]
    public class GhostDifficultyList : ScriptableObject
    {
        [SerializeField] private GhostDifficultyData[] ghostDifficultyData;

        public IReadOnlyList<GhostDifficultyData> GhostDifficultyData => ghostDifficultyData;
    }
}