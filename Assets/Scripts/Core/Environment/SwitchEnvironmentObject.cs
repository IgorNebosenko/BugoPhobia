using System;
using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class SwitchEnvironmentObject : EnvironmentObjectBase, ISwitchInteractable
    {
        [SerializeField] private Vector3 eulerDisabledPosition;
        [SerializeField] private Vector3 eulerEnabledPosition;
        [SerializeField] private Transform switchTransform;
        
        [field: SerializeField] public bool IsOn { get; private set; }

        public bool IsElectricityOn => IsOn;

        public event Action<bool, bool> Switch;

        public override void OnInteract()
        {
            IsOn = !IsOn;

            switchTransform.localEulerAngles = IsOn ? eulerEnabledPosition : eulerDisabledPosition;
            
            Switch?.Invoke(IsOn, false);
        }

        public void SwitchOn()
        {
            Debug.Log("Switch On");
            if (IsOn)
                return;
            
            OnInteract();
        }

        public void SwitchOff()
        {
            Debug.Log("Switch Off");
            if (!IsOn)
                return;
            
            OnInteract();
        }
    }
}