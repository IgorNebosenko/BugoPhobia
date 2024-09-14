using System;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class SwitchEnvironmentObject : EnvironmentObjectBase
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
    }
}