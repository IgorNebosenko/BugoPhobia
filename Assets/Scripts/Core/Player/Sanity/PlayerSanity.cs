using UnityEngine;

namespace ElectrumGames.Core.Player.Sanity
{
    public class PlayerSanity : ISanity
    {
        private const float MinSanity = 0f;
        private const float MaxSanity = 100f;
        
        public float Sanity { get; private set; }

        public PlayerSanity(float initialSanity, int ownerId)
        {
            ChangeSanity(initialSanity, ownerId);
        }
        
        public void ChangeSanity(float value, int ownerId)
        {
            var newValue = Sanity + value;

            if (newValue <= MinSanity)
                Sanity = MinSanity;
            else if (newValue >= MaxSanity)
                Sanity = MaxSanity;
            else
                Sanity = newValue;
            
            Debug.Log($"Current sanity is: {(int)Sanity}");
        }

        public void GetGhostEvent(float minGhostEventDrainSanity, float maxGhostEventDrainSanity, int ownerId)
        {
            var decreaseSanity = Random.Range(maxGhostEventDrainSanity, minGhostEventDrainSanity);
            
            ChangeSanity(decreaseSanity, ownerId);
        }
    }
}