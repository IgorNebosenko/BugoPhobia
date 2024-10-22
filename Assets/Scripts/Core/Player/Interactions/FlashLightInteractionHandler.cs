using UnityEngine;

namespace Core.Player.Interactions
{
    public class FlashLightInteractionHandler : MonoBehaviour
    {
        [SerializeField] private Light smallLight;
        [SerializeField] private Light mediumLight;
        [SerializeField] private Light bigLight;

        public void EnableSmallLight()
        {
            smallLight.enabled = true;
            mediumLight.enabled = false;
            bigLight.enabled = false;
        }
        
        public void EnableMediumLight()
        {
            smallLight.enabled = false;
            mediumLight.enabled = true;
            bigLight.enabled = false;
        }
        
        public void EnableBigLight()
        {
            smallLight.enabled = false;
            mediumLight.enabled = false;
            bigLight.enabled = true;
        }
        
        public void DisableLight()
        {
            smallLight.enabled = false;
            mediumLight.enabled = false;
            bigLight.enabled = false;
        }
    }
}