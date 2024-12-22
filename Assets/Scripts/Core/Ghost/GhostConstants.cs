using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Ghost
{
    public readonly struct GhostConstants
    {
        public readonly DistanceMoving distanceMoving;
        
        public readonly float defaultSanityStartHunting;
        public readonly float modifiedSanityStartHunting;

        public readonly float defaultHuntingSpeed;
        public readonly bool hasSpeedUp;

        public readonly GhostVisibility ghostVisibility;
        public readonly float huntCooldown;
        public readonly float ghostEventCooldown;
        public readonly float abilityCooldown;

        public readonly float abilityChance;
        
        public readonly float minDoorAngle;
        public readonly float maxDoorAngle;
        public readonly float minDoorTouchTime;
        public readonly float maxDoorTouchTime;

        public GhostConstants(DistanceMoving distanceMoving, float defaultSanityStartHunting, float modifiedSanityStartHunting,
            float defaultHuntingSpeed, bool hasSpeedUp, GhostVisibility ghostVisibility,
            float huntCooldown, float ghostEventCooldown, float abilityCooldown, float abilityChance,
            float minDoorAngle, float maxDoorAngle, float minDoorTouchTime, float maxDoorTouchTime)
        {
            this.distanceMoving = distanceMoving;
            this.defaultSanityStartHunting = defaultSanityStartHunting;
            this.modifiedSanityStartHunting = modifiedSanityStartHunting;
            this.defaultHuntingSpeed = defaultHuntingSpeed;
            this.hasSpeedUp = hasSpeedUp;
            this.ghostVisibility = ghostVisibility;
            this.huntCooldown = huntCooldown;
            this.ghostEventCooldown = ghostEventCooldown;
            this.abilityCooldown = abilityCooldown;
            this.abilityChance = abilityChance;
            this.minDoorAngle = minDoorAngle;
            this.maxDoorAngle = maxDoorAngle;
            this.minDoorTouchTime = minDoorTouchTime;
            this.maxDoorTouchTime = maxDoorTouchTime;
        }

        public static GhostConstants Empty()
        {
            return new GhostConstants(DistanceMoving.Minimal, 0, 0, 
                0, false, GhostVisibility.Invisible, 0, 0, 
                0, 0, 0, 0, 0, 0);
        }

        public override string ToString()
        {
            return "Distance moving: " + $"{distanceMoving}\n" +
                   "-----\n" +
                   "Default sanity start hunting: " + $"{defaultSanityStartHunting}\n" +
                   "Modified sanity start hunting:" + $"{modifiedSanityStartHunting}\n" +
                   "-----\n" +
                   "Hunting speed: " + $"{defaultHuntingSpeed}\n" +
                   "Has speed up: " + $"{hasSpeedUp}\n" +
                   "-----\n" +
                   "Ghost visibility: " + $"{ghostVisibility}\n" +
                   "-----\n" +
                   "Hunt cooldown: " + $"{huntCooldown}\n" +
                   "Ghost event cooldown: " + $"{ghostEventCooldown}\n" +
                   "Ability cooldown: " + $"{abilityCooldown}\n" +
                   "-----\n" +
                   "Ability chance: " + $"{abilityChance}\n\n";

        }
    }
}