using DG.Tweening;
using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class DoorEnvironmentObject : EnvironmentObjectBase, IDoorInteractable
    {
        [SerializeField] private float minAngle = 0f;
        [SerializeField] private float maxAngle = 90f;
        [SerializeField] private float openedForce = 1f;
        [SerializeField] private float openSpeed = 0.1f;
        
        private bool _isLocked;
        private float _initialAngle;

        public Transform Transform => transform;
        [field: SerializeField] public bool DoorWithLock { get; private set; }
        [field: SerializeField] public float CurrentAngle { get; private set; }

        private void Start()
        {
            _isLocked = DoorWithLock;
            _initialAngle = transform.localEulerAngles.y;
        }

        public void SetCameraAngleAndInteract(Vector2 inputLook)
        {
            if (_isLocked)
                return;
            
            var inputForce = inputLook.x * openedForce * -1;
            
            CurrentAngle += inputForce;
            
            if (CurrentAngle < minAngle)//Mathf.Clamp() works incorrect
                CurrentAngle = minAngle;
            else if (CurrentAngle > maxAngle)
                CurrentAngle = maxAngle;

            transform.DOLocalRotate(Vector3.up * CurrentAngle, openSpeed);
        }

        public void LockDoor()
        {
            _isLocked = true;
            CloseDoor();
        }

        public void UnlockDoor()
        {
            _isLocked = false;
        }

        public void CloseDoor()
        {
            CurrentAngle = _initialAngle;
            
            transform.DOLocalRotate(Vector3.up * CurrentAngle, openSpeed);
        }

        public override void OnInteract()
        {
        }

        public void TouchDoor(float angle, float time)
        {
            Debug.LogWarning("Add UV print if this evidence exists");
            
            var canOpen = CurrentAngle + angle < maxAngle;
            var canClose = CurrentAngle - angle > minAngle;

            if (canOpen && canClose)
            {
                canOpen = Random.Range(0, 2) != 0;
            }

            if (canOpen)
                CurrentAngle += angle;
            else
                CurrentAngle -= angle;

            transform.DOLocalRotate(Vector3.up * CurrentAngle, time);
        }
    }
}