using DG.Tweening;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class DoorEnvironmentObject : EnvironmentObjectBase
    {
        [SerializeField] private float minAngle = 0f;
        [SerializeField] private float maxAngle = 90f;
        [SerializeField] private float openedForce = 1f;
        [SerializeField] private float openSpeed = 0.1f;
        
        private bool _isLocked;
        
        [field: SerializeField] public bool DoorWithLock { get; private set; }
        [field: SerializeField] public float CurrentAngle { get; private set; }

        private void Start()
        {
            _isLocked = DoorWithLock;
        }

        public override void OnInteract()
        {
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
            CurrentAngle = minAngle;

            transform.DOLocalRotate(Vector3.up * CurrentAngle, openSpeed);
        }

        public void UnlockDoor()
        {
            _isLocked = false;
        }

        public void OpenDoor()
        {
            _isLocked = false;
        }
    }
}