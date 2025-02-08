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

        public event Action<bool> StateChanged;

        private bool _lockState;
        public bool State { get; private set; }

        private void Start()
        {
            if (initialStateOn)
                OnInteract();
            
            if (isBroken)
                StateChanged?.Invoke(false);
        }

        public override void OnInteract()
        {
            if (_lockState)
                return;

            _lockState = true;
            State = !State;
            
            handle.DOLocalRotate(State ? eulerHandleOn : eulerHandleOff, switchTime).OnComplete(() =>
            {
                if (!isBroken)
                    StateChanged?.Invoke(State);
                _lockState = false;
            });
        }
    }
}
