using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Ghost
{
    public readonly struct GhostVariables
    {
        public readonly GhostType ghostType;

        public readonly bool isMale;
        public readonly int age;
        
        public readonly float throws;
        public readonly float doorsInteractions;
        public readonly float switchesInteractions;
        public readonly float otherInteractions;

        public readonly float ghostEvents;

        public GhostVariables(GhostType ghostType, bool isMale, int age, float throws, float doorsInteractions, 
            float switchesInteractions, float otherInteractions, float ghostEvents)
        {
            this.ghostType = ghostType;
            this.isMale = isMale;
            this.age = age;
            this.throws = throws;
            this.doorsInteractions = doorsInteractions;
            this.switchesInteractions = switchesInteractions;
            this.otherInteractions = otherInteractions;
            this.ghostEvents = ghostEvents;
        }

        public static GhostVariables Empty()
        {
            return new GhostVariables(GhostType.None, false, 0, 0, 0, 0,
                0, 0);
        }

        public override string ToString()
        {
            return $"Ghost type: {ghostType}\n" +
                   "-----\n" +
                   "Is male :" + $"{isMale}\n" +
                   "Age: " + $"{age}\n" +
                   "-----\n" +
                   "Throws: " + $"{throws}\n" +
                   "Doors interactions: " + $"{doorsInteractions}\n" +
                   "Switches interactions: " + $"{switchesInteractions}\n" +
                   "Other interactions: " + $"{otherInteractions}\n" +
                   "-----\n" +
                   "Ghost events: " + $"{ghostEvents}\n\n";
        }
    }
}