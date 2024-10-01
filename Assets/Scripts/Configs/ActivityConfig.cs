using System;
using System.Collections.Generic;
using ElectrumGames.GlobalEnums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ElectrumGames.Configs
{
    [Serializable]
    public class GhostActivityData
    {
        [field: SerializeField] public GhostType GhostType { get; private set; }
        [Space]
        [SerializeField, MinMaxSlider(0, 1, true)] private Vector2 throws;
        [Space]
        [SerializeField, MinMaxSlider(0, 1, true)] private Vector2 doorsInteractions;
        [SerializeField, MinMaxSlider(0, 1, true)] private Vector2 switchesInteractions;
        [SerializeField, MinMaxSlider(0, 1, true)] private Vector2 otherInteractions;
        [Space]
        [SerializeField, MinMaxSlider(0, 1, true)] private Vector2 ghostEvents;
        [field: Space]
        [field: SerializeField] public DistanceMoving DistanceMoving { get; private set; }
        [field: SerializeField] public float DefaultNonHuntSpeed { get; private set; } = 1.8f;
        [field: SerializeField] public float MaxGhostSpeed { get; private set; } = 3f;
        [field: Space]
        [field: SerializeField, Range(0, 100)] public float DefaultSanityStartHunting { get; private set; }
        [field: SerializeField, Range(0, 100)] public float ModifiedSanityStartHunting { get; private set; }
        [field: Space]
        [field: SerializeField, Range(0f, 3f)] public float DefaultHuntingSpeed { get; private set; }
        [field: SerializeField] public bool HasSpeedUp { get; private set; }
        [field: Space]
        [field: SerializeField] public GhostVisibility GhostVisibility { get; private set; }
        [field: Space]
        [field: SerializeField] public float HuntCooldown { get; private set; }
        [field: SerializeField] public float GhostEventCooldown { get; private set; }
        [field: SerializeField] public float AbilityCooldown { get; private set; }
        [field: Space]
        [field: SerializeField, Range(0f, 1f)] public float AbilityChance { get; private set; }
        [field: Space] 
        [SerializeField, MinMaxSlider(0f, 45f, true)] private Vector2 doorAngle;
        [SerializeField, MinMaxSlider(0f, 3f, true)] private Vector2 doorTouchTime;
        [field: Space]
        [field: SerializeField, Range(0f, 1f)] public float ChanceOnSwitch { get; private set; }
        [field: Space]
        [field: SerializeField] public float ThrownForce { get; private set; }

        public float ThrowsMin => throws.x;
        public float ThrowsMax => throws.y;
        
        public float DoorsInteractionsMin => doorsInteractions.x;
        public float DoorsInteractionsMax => doorsInteractions.y;
        
        public float SwitchesInteractionsMin => switchesInteractions.x;
        public float SwitchesInteractionsMax => switchesInteractions.y;
        
        public float OtherInteractionsMin => otherInteractions.x;
        public float OtherInteractionsMax => otherInteractions.y;
        
        public float GhostEventsMin => ghostEvents.x;
        public float GhostEventsMax => ghostEvents.y;

        public float MinDoorAngle => doorAngle.x;
        public float MaxDoorAngle => doorAngle.y;

        public float MinDoorTouchTime => doorTouchTime.x;
        public float MaxDoorTouchTime => doorTouchTime.y;
    }

    [CreateAssetMenu(fileName = "ActivityConfig", menuName = "Ghosts configs/Activity Config")]
    public class ActivityConfig : ScriptableObject
    {
        [ListDrawerSettings(NumberOfItemsPerPage = 1)] 
        [SerializeField]
        private List<GhostActivityData> ghostActivities;
        
        public IReadOnlyList<GhostActivityData> GhostActivities => ghostActivities;
    }
}