using DG.Tweening;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.MVP.Managers;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Boards
{
    public class BoardsHandler : MonoBehaviour
    {
        [SerializeField] private Camera boardCamera;
        [Space]
        [SerializeField] private BoardEnvironmentObject mainBoard;
        [SerializeField] private Transform mainBoardCameraTransform;
        [Space]
        [SerializeField] private BoardEnvironmentObject additionalBoard;
        [SerializeField] private Transform additionalBoardCameraTransform;
        [Space]
        [SerializeField] private float lerpTime = 0.5f;
        [SerializeField] private float targetFov = 60f;

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

        public void OnMainBoardClicked()
        {
            CommonOnClickAction();

            mainBoard.BoardCollider.enabled = false;
            
            var sequence = DOTween.Sequence();
            sequence.Join(boardCamera.transform.DOMove(mainBoardCameraTransform.position, lerpTime))
                .Join(boardCamera.transform.DORotate(mainBoardCameraTransform.rotation.eulerAngles, lerpTime))
                .Join(boardCamera.DOFieldOfView(targetFov, lerpTime));
        }

        public void OnAdditionalBoardClicked()
        {
            CommonOnClickAction();

            additionalBoard.BoardCollider.enabled = false;
            
            var sequence = DOTween.Sequence();
            sequence.Join(boardCamera.transform.DOMove(additionalBoardCameraTransform.position, lerpTime))
                .Join(boardCamera.transform.DORotate(additionalBoardCameraTransform.rotation.eulerAngles, lerpTime))
                .Join(boardCamera.DOFieldOfView(targetFov, lerpTime));
        }

        private void CommonOnClickAction()
        {
            _inputActions.Moving.Disable();
            _inputActions.Interactions.Disable();
            _inputActions.UI.Disable();
            
            _viewManager.CloseRootView();
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            _mainCamera.enabled = false;
            boardCamera.enabled = true;
            
            boardCamera.transform.position = _mainCamera.transform.position;
            boardCamera.transform.rotation = _mainCamera.transform.rotation;
            boardCamera.fieldOfView = _mainCamera.fieldOfView;
        }
    }
}