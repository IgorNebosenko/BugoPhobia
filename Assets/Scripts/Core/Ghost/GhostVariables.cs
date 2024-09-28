using ElectrumGames.GlobalEnums;

namespace ElectrumGames.Core.Ghost
{
    public class GhostVariables
    {
        public GhostType ghostType;

        public bool isMale;
        public int age;
        
        public float throws;
        public float doorsInteractions;
        public float switchesInteractions;
        public float otherInteractions;

        public float ghostEvents;

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