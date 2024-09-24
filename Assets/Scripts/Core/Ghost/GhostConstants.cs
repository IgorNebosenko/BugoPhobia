using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Ghost
{
    public class GhostConstants
    {
        public DistanceMoving distanceMoving;
        
        public float defaultSanityStartHunting;
        public float modifiedSanityStartHunting;

        public float defaultHuntingSpeed;
        public bool hasSpeedUp;

        public GhostVisibility ghostVisibility;
        public float huntCooldown;
        public float ghostEventCooldown;
        public float abilityCooldown;

        public float abilityChance;

        public GhostConstants(DistanceMoving distanceMoving, float defaultSanityStartHunting, float modifiedSanityStartHunting, 
            float defaultHuntingSpeed, bool hasSpeedUp, GhostVisibility ghostVisibility, float huntCooldown, 
            float ghostEventCooldown, float abilityCooldown, float abilityChance)
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