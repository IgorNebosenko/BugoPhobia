using System;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class SwitchEnvironmentObject : MonoBehaviour
    {
        [SerializeField] private Vector3 eulerDisabledPosition;
        [SerializeField] private Vector3 eulerEnabledPosition;
        [SerializeField] private Transform switchTransform;
        
        [field: SerializeField] public bool IsOn { get; private set; }

        public event Action<bool> Switch;

        public void SwitchState()
        {
            IsOn = !IsOn;

            switchTransform.localEulerAngles = IsOn ? eulerEnabledPosition : eulerDisabledPosition;
            
            Switch?.Invoke(IsOn);
        }
    }
}