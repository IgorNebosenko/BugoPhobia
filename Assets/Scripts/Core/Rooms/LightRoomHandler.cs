using ElectrumGames.Core.Environment;
using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class LightRoomHandler : MonoBehaviour
    {
        [SerializeField] private SwitchEnvironmentObject roomSwitch;
        [SerializeField] private LightEnvironmentObject[] lightEnvironmentObjects;
        [SerializeField] private LightEnvironmentObject[] switchableLamps;
        [Space]
        [SerializeField] private float flickerSpeedMin = 0.1f;
        [SerializeField] private float flickerSpeedMax = 0.25f;

        public bool IsLightOn { get; private set; }
        
        private void OnEnable()
        {
            roomSwitch.Switch += ChangeState;
        }

        private void ChangeState(bool state, bool includeSwitchableLamps = false)
        {
            IsLightOn = state;
            
            for (var i = 0; i < lightEnvironmentObjects.Length; i++)
            {
                lightEnvironmentObjects[i].SwitchStateTo(IsLightOn);
            }

            if (includeSwitchableLamps)
            {
                for (var i = 0; i < switchableLamps.Length; i++)
                    switchableLamps[i].SwitchStateTo(IsLightOn);
            }
        }

        public void DoFlick()
        {
            for (var i = 0; i < lightEnvironmentObjects.Length; i++)
            {
                lightEnvironmentObjects[i].DoFlick(flickerSpeedMin, flickerSpeedMax);
            }
            for (var i = 0; i < switchableLamps.Length; i++)
            {
                switchableLamps[i].DoFlick(flickerSpeedMin, flickerSpeedMax);
            }
        }
    }
}