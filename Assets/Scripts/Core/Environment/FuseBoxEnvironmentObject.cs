using System;
using DG.Tweening;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class FuseBoxEnvironmentObject : EnvironmentObjectBase
    {
        [SerializeField] private Transform handle;
        [Space]
        [SerializeField] private Vector3 eulerHandleOn;
        [SerializeField] private Vector3 eulerHandleOff;
        [SerializeField] private float switchTime = 0.5f;

        public event Action<bool> SwitchChanged;

        private bool _lockState;
        private bool _state;

        public override void OnInteract()
        {
            if (_lockState)
                return;

            _lockState = true;
            _state = !_state;
            
            handle.DOLocalRotate(_state ? eulerHandleOn : eulerHandleOff, switchTime).OnComplete(() =>
            {
                SwitchChanged?.Invoke(_state);
                _lockState = false;
            });
        }
    }
}
