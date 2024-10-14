using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;
using Sirenix.OdinInspector;
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
        [field: SerializeField, Range(0f, 1f)] public float RedLightChance { get; private set; } = 0.2f;
        [field: SerializeField, Range(0f, 1f)] public float DoorsCloseChance { get; private set; } = 0.3f;
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
        [field: SerializeField] public float HuntChance { get; private set; } = 0.7f;
        [field: SerializeField] public float SafeHuntTime { get; private set; } = 3f;
        [field: SerializeField] public float HuntDuration { get; private set; } = 35f;
        [field: SerializeField] public float SpeedUpByIteration { get; private set; } = 0.025f;
        [field: Space]
        [field: SerializeField] public float DefaultSanity { get; private set; } = 100f;
        [field: SerializeField] public float DefaultDrainSanity { get; private set; } = -0.25f;
        [field: SerializeField] public float MinGhostEventDrainSanity { get; private set; } = -10f;
        [field: SerializeField] public float MaxGhostEventDrainSanity { get; private set; } = -15f;
        [field: SerializeField] public float GhostEventDrainOnKillOrDisconnectMate { get; private set; } = -15f;
    }

    [CreateAssetMenu(fileName = "GhostDifficultyList", menuName = "Ghosts configs/Ghost difficulty list")]
    public class GhostDifficultyList : ScriptableObject
    {
        [SerializeField, ListDrawerSettings(NumberOfItemsPerPage = 1)] private GhostDifficultyData[] ghostDifficultyData;

        public IReadOnlyList<GhostDifficultyData> GhostDifficultyData => ghostDifficultyData;
    }
}