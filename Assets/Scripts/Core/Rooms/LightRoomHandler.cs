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

        private bool _redLight;
        
        public bool IsLightOn { get; private set; }
        public bool RedLight
        {
            set
            {
                for (var i = 0; i < lightEnvironmentObjects.Length; i++)
                {
                    lightEnvironmentObjects[i].SetRedState(value);
                }

                for (var i = 0; i < switchableLamps.Length; i++)
                {
                    switchableLamps[i].SetRedState(value);
                }
                
                _redLight = value;
            }
        }

        private void Start()
        {
            IsLightOn = roomSwitch.IsElectricityOn;
        }

        private void OnEnable()
        {
            roomSwitch.Switch += ChangeState;
        }

        private void ChangeState(bool state, bool includeSwitchableLamps = false)
        {
            IsLightOn = state;
            
            if (IsLightOn)
                roomSwitch.SwitchOn();
            else
                roomSwitch.SwitchOff();
            
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
                lightEnvironmentObjects[i]?.DoFlick(flickerSpeedMin, flickerSpeedMax, _redLight);
            }
            for (var i = 0; i < switchableLamps.Length; i++)
            {
                switchableLamps[i]?.DoFlick(flickerSpeedMin, flickerSpeedMax, _redLight);
            }
        }

        public void SwitchOnLight()
        {
            ChangeState(true, false);
        }

        public void SwitchOffLight()
        {
            ChangeState(false, true);
        }
    }
}