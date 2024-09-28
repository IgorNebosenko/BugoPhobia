using System.Linq;
using ElectrumGames.Configs;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Ghost
{
    public class GhostEnvironmentHandler
    {
        public GhostVariables GhostVariables { get; private set; }
        public GhostConstants GhostConstants { get; private set; }
        public int GhostRoomId { get; private set; }
        
        private readonly ActivityConfig _activityConfig;
        
        public GhostEnvironmentHandler(ActivityConfig activityConfig)
        {
            _activityConfig = activityConfig;
        }

        public void InitGhost(int minGhostType, int maxGhostType, int minRoomId, int maxRoomId)
        {
            //var ghostType = (GhostType)Random.Range(minGhostType, maxGhostType);
            var ghostType = GhostType.Blaze;
            Debug.LogWarning("Ghost type set as explicit Blaze!");
            
            var activityData = _activityConfig.GhostActivities.First(x => x.GhostType == ghostType);
            
            GhostVariables = new GhostVariables(ghostType, Random.Range(0, 2) != 0, Random.Range(0, 1000),
                Random.Range(activityData.ThrowsMin, activityData.ThrowsMax),
                Random.Range(activityData.DoorsInteractionsMin, activityData.DoorsInteractionsMax),
                Random.Range(activityData.SwitchesInteractionsMin, activityData.SwitchesInteractionsMax),
                Random.Range(activityData.OtherInteractionsMin, activityData.OtherInteractionsMax),
                Random.Range(activityData.GhostEventsMin, activityData.GhostEventsMax));

            GhostConstants = new GhostConstants(activityData.DistanceMoving, activityData.DefaultSanityStartHunting,
                activityData.ModifiedSanityStartHunting, activityData.DefaultHuntingSpeed, activityData.HasSpeedUp,
                activityData.GhostVisibility, activityData.HuntCooldown, activityData.GhostEventCooldown,
                activityData.AbilityCooldown, activityData.AbilityChance);
            
            GhostRoomId = Random.Range(minRoomId, maxRoomId);
            
            Debug.Log(GhostVariables);
            Debug.Log(GhostConstants);
            Debug.Log($"Room id: {GhostRoomId}");
        }
    }
}