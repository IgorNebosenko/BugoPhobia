using System;
using ElectrumGames.Core.Environment;
using UnityEngine;
using Zenject;

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

        private FuseBoxEnvironmentObject _boxEnvironmentObject;
        
        public bool IsLightOn { get; private set; }
        public bool IsElectricityOn { get; private set; }
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

        [Inject]
        private void Construct(FuseBoxEnvironmentObject boxEnvironmentObject)
        {
            _boxEnvironmentObject = boxEnvironmentObject;

            for (var i = 0; i < lightEnvironmentObjects.Length; i++)
            {
                lightEnvironmentObjects[i].FuseBoxConnect(_boxEnvironmentObject);
            }

            for (var i = 0; i < switchableLamps.Length; i++)
            {
                switchableLamps[i].FuseBoxConnect(_boxEnvironmentObject);
            }
        }
        
        private void Start()
        {
            if (roomSwitch == null)
                return;
            
            IsLightOn = roomSwitch.IsElectricityOn;
        }

        private void OnEnable()
        {
            if (roomSwitch == null)
                return;
            
            roomSwitch.Switch += ChangeState;
            roomSwitch.FlickLight += DoFlick;
        }

        private void OnDisable()
        {
            if (roomSwitch == null)
                return;
            
            roomSwitch.Switch -= ChangeState;
            roomSwitch.FlickLight -= DoFlick;
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

        public void LockSwitch()
        {
            roomSwitch.SetLockState(true);
        }

        public void UnlockSwitch()
        {
            roomSwitch.SetLockState(false);
        }
    }
}