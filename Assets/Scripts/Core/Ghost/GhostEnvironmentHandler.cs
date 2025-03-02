using System.Linq;
using ElectrumGames.Configs;
using ElectrumGames.DebugElements;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Ghost
{
    public class GhostEnvironmentHandler
    {
        public GhostVariables GhostVariables { get; private set; } = GhostVariables.Empty();
        public GhostConstants GhostConstants { get; private set; } = GhostConstants.Empty();
        public int GhostRoomId { get; private set; }
        
        public readonly ActivityConfig _activityConfig;
        
        public GhostEnvironmentHandler(ActivityConfig activityConfig)
        {
            _activityConfig = activityConfig;
        }

        public void InitGhost(int minGhostType, int maxGhostType, int minRoomId, int maxRoomId)
        {
            GhostType ghostType;
            if (CheatVariables.SelectedGhostType == GhostType.None)
                ghostType = (GhostType) Random.Range(minGhostType, maxGhostType);
            else
                ghostType = CheatVariables.SelectedGhostType;
            
            if (CheatVariables.RoomId == -1)
                GhostRoomId = Random.Range(minRoomId, maxRoomId);
            else
                GhostRoomId = CheatVariables.RoomId;
            
            var activityData = _activityConfig.GhostActivities.First(x => x.GhostType == ghostType);
            
            GhostVariables = new GhostVariables(ghostType, Random.Range(0, 2) != 0, Random.Range(0, 100),//<-
                Random.Range(activityData.ThrowsMin, activityData.ThrowsMax),
                Random.Range(activityData.DoorsInteractionsMin, activityData.DoorsInteractionsMax),
                Random.Range(activityData.SwitchesInteractionsMin, activityData.SwitchesInteractionsMax),
                Random.Range(activityData.OtherInteractionsMin, activityData.OtherInteractionsMax),
                Random.Range(activityData.GhostEventsMin, activityData.GhostEventsMax),
                    GhostRoomId);

            GhostConstants = new GhostConstants(activityData.DistanceMoving, activityData.DefaultSanityStartHunting,
                activityData.ModifiedSanityStartHunting, activityData.DefaultHuntingSpeed, activityData.HasSpeedUp,
                activityData.GhostVisibility, activityData.HuntCooldown, activityData.GhostEventCooldown,
                activityData.AbilityCooldown, activityData.AbilityChance, activityData.MinDoorAngle,
                activityData.MaxDoorAngle, activityData.MinDoorTouchTime, activityData.MaxDoorTouchTime,
                activityData.ChanceShutDownFuseBox, activityData.CooldownShutDown, activityData.CanActivateFuseBoxByInteraction);
            
            Debug.Log(GhostVariables);
            Debug.Log(GhostConstants);
            Debug.Log($"Room id: {GhostRoomId}");
        }
    }
}