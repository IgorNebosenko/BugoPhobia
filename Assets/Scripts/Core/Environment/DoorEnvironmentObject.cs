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
        
        [field: SerializeField] public float CurrentAngle { get; private set; }
        
        public override void OnInteract()
        {
        }

        public void SetCameraAngleAndInteract(Vector2 inputLook)
        {
            var inputForce = inputLook.x * openedForce;
            
            CurrentAngle += inputForce;
            
            if (CurrentAngle < minAngle)//Mathf.Clamp() works incorrect
                CurrentAngle = minAngle;
            else if (CurrentAngle > maxAngle)
                CurrentAngle = maxAngle;

            transform.DOLocalRotate(Vector3.up * CurrentAngle, openSpeed);
        }
    }
}