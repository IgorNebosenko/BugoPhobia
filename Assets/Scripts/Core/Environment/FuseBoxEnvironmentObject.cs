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
        [Space]
        [SerializeField] private bool initialStateOn;
        [SerializeField] private bool isBroken;

        public event Action<bool> FuseBoxChanged;

        private bool _lockState;
        private bool _state;

        private void Start()
        {
            if (initialStateOn)
                OnInteract();
            
            if (isBroken)
                FuseBoxChanged?.Invoke(false);
        }

        public override void OnInteract()
        {
            if (_lockState)
                return;

            _lockState = true;
            _state = !_state;
            
            handle.DOLocalRotate(_state ? eulerHandleOn : eulerHandleOff, switchTime).OnComplete(() =>
            {
                if (!isBroken)
                    FuseBoxChanged?.Invoke(_state);
                _lockState = false;
            });
        }
    }
}
