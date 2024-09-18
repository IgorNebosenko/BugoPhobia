using DG.Tweening;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Environment
{
    public class BoardEnvironmentObject : EnvironmentObjectBase
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Camera boardCamera;
        [SerializeField] private float targetFov = 60;
        [Space]
        [SerializeField] private float lerpTime = 0.5f;
        [SerializeField] private float tolerance = 0.01f;
        [Space]
        [SerializeField] private Collider boardCollider;
        
        private Camera _mainCamera;
        private InputActions _inputActions;
        private ViewManager _viewManager;

        [Inject]
        private void Construct(Camera mainCamera, InputActions inputActions, ViewManager viewManager)
        {
            _mainCamera = mainCamera;
            _inputActions = inputActions;
            
            _viewManager = viewManager;
        }

        private void Update()
        {
            _mainCamera.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        public override void OnInteract()
        {
            _inputActions.Moving.Disable();
            _inputActions.Interactions.Disable();
            
            _viewManager.CloseRootView();
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            var sequence = DOTween.Sequence();
            sequence.Join(_mainCamera.transform.DOMove(cameraTransform.position, lerpTime))
                .Join(_mainCamera.transform.DORotate(cameraTransform.rotation.eulerAngles, lerpTime))
                .Join(_mainCamera.DOFieldOfView(60, lerpTime));
        }

        public void OnCancel()
        {
        }
    }
}