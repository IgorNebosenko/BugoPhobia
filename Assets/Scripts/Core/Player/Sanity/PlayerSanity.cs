using UnityEngine;

namespace ElectrumGames.Core.Player.Sanity
{
    public class PlayerSanity : ISanity
    {
        private const float MinSanity = 0f;
        private const float MaxSanity = 100f;
        
        public float CurrentSanity { get; private set; }

        public PlayerSanity(float initialSanity, int ownerId)
        {
            ChangeSanity(initialSanity, ownerId);
        }
        
        public void ChangeSanity(float value, int ownerId)
        {
            var newValue = CurrentSanity + value;

            if (newValue <= MinSanity)
                CurrentSanity = MinSanity;
            else if (newValue >= MaxSanity)
                CurrentSanity = MaxSanity;
            else
                CurrentSanity = newValue;
            
            Debug.Log($"Current sanity is: {(int)CurrentSanity}");
        }

        public void GetGhostEvent(float minGhostEventDrainSanity, float maxGhostEventDrainSanity, int ownerId)
        {
            var decreaseSanity = Random.Range(maxGhostEventDrainSanity, minGhostEventDrainSanity);
            
            ChangeSanity(decreaseSanity, ownerId);
        }
    }
}