using System;
using DG.Tweening;
using ElectrumGames.Core.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Environment
{
    public class FuseBoxEnvironmentObject : EnvironmentObjectBase, IFuseBoxInteractable
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
        
        public Transform FuseBoxTransform => transform;

        private void Awake()
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

        public bool TryInteract(bool canSwitchOn)
        {
            var state = canSwitchOn && Random.Range(0, 2) != 0;
            
            if (State == state) 
                return false;
            
            OnInteract();
            return true;
        }

        public void ShutDown()
        {
            if (State)
                OnInteract();
        }
    }
}
