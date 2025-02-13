using System;
using ElectrumGames.Core.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Environment
{
    public class SwitchEnvironmentObject : EnvironmentObjectBase, ISwitchInteractable
    {
        [SerializeField] private Vector3 eulerDisabledPosition;
        [SerializeField] private Vector3 eulerEnabledPosition;
        [SerializeField] private Transform switchTransform;

        private bool _isLocked;
        
        [field: SerializeField] public bool IsOn { get; private set; }
        
        public Transform Transform => transform;

        public bool IsElectricityOn => IsOn;

        public event Action<bool, bool> Switch;
        public event Action FlickLight;

        public override void OnInteract()
        {
            if (_isLocked)
                return;
            
            IsOn = !IsOn;

            switchTransform.localEulerAngles = IsOn ? eulerEnabledPosition : eulerDisabledPosition;
            
            Switch?.Invoke(IsOn, false);
        }

        public void SwitchOn()
        {
            if (IsOn)
                return;
            
            OnInteract();
        }
        
        public void SwitchOff()
        {
            if (!IsOn)
                return;
            
            OnInteract();
        }
        
        public void TrySwitchOffByGhost()
        {
            if (IsOn)
                return;

            if (Random.Range(0, 2) != 0)
            {
                FlickLight?.Invoke();
            }
            else
                SwitchOff();
        }

        public void SetLockState(bool state)
        {
            _isLocked = state;
        }
    }
}