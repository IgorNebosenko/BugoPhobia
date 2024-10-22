using UnityEngine;

namespace ElectrumGames.Core.Player.Interactions.Items
{
    public class FlashLightInteractionHandler : MonoBehaviour
    {
        [SerializeField] private Light smallLight;
        [SerializeField] private Light mediumLight;
        [SerializeField] private Light bigLight;
        
        public bool IsElectricityOn { get; private set; }

        public void EnableSmallLight()
        {
            smallLight.enabled = true;
            mediumLight.enabled = false;
            bigLight.enabled = false;

            IsElectricityOn = true;
        }
        
        public void EnableMediumLight()
        {
            smallLight.enabled = false;
            mediumLight.enabled = true;
            bigLight.enabled = false;
            
            IsElectricityOn = true;
        }
        
        public void EnableBigLight()
        {
            smallLight.enabled = false;
            mediumLight.enabled = false;
            bigLight.enabled = true;
            
            IsElectricityOn = true;
        }
        
        public void DisableLight()
        {
            smallLight.enabled = false;
            mediumLight.enabled = false;
            bigLight.enabled = false;
            
            IsElectricityOn = false;
        }
    }
}