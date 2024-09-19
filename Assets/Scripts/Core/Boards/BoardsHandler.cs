using DG.Tweening;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
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
        [SerializeField] private BoardInputHandler boardInputHandler;
        [Space]
        [SerializeField] private float lerpTime = 0.5f;
        [SerializeField] private float targetFov = 60f;

        private Camera _mainCamera;
        private InputActions _inputActions;
        private ViewManager _viewManager;

        public BoardLobbyState State { get; private set; } = BoardLobbyState.Unselected;

        [Inject]
        private void Construct(Camera mainCamera, InputActions inputActions, ViewManager viewManager)
        {
            _mainCamera = mainCamera;
            _inputActions = inputActions;
            
            _viewManager = viewManager;
        }

        private void FixedUpdate()
        {
            if (State == BoardLobbyState.Unselected && boardInputHandler.Open)
            {
                OnMainBoardClicked();
            }
            if ((State == BoardLobbyState.MainBoardSelected || State == BoardLobbyState.AdditionalBoardSelected) 
                && boardInputHandler.Return)
            {
                OnReturnToMainCamera();
            }
            if (State == BoardLobbyState.MainBoardSelected && boardInputHandler.MoveRight)
            {
                OnToAdditionalBoardMove();
            }
            if (State == BoardLobbyState.AdditionalBoardSelected && boardInputHandler.MoveLeft)
            {
                OnToMainBoardMove();
            }
        }

        public void OnMainBoardClicked()
        {
            CommonOnClickAction();

            mainBoard.BoardCollider.enabled = false;
            
            var sequence = DOTween.Sequence();
            sequence.Join(boardCamera.transform.DOMove(mainBoardCameraTransform.position, lerpTime))
                .Join(boardCamera.transform.DORotate(mainBoardCameraTransform.rotation.eulerAngles, lerpTime))
                .Join(boardCamera.DOFieldOfView(targetFov, lerpTime));

            sequence.OnComplete(() => State = BoardLobbyState.MainBoardSelected);
        }

        public void OnAdditionalBoardClicked()
        {
            CommonOnClickAction();

            additionalBoard.BoardCollider.enabled = false;
            
            var sequence = DOTween.Sequence();
            sequence.Join(boardCamera.transform.DOMove(additionalBoardCameraTransform.position, lerpTime))
                .Join(boardCamera.transform.DORotate(additionalBoardCameraTransform.rotation.eulerAngles, lerpTime))
                .Join(boardCamera.DOFieldOfView(targetFov, lerpTime));

            sequence.OnComplete(() => State = BoardLobbyState.AdditionalBoardSelected);
        }

        private void CommonOnClickAction()
        {
            State = BoardLobbyState.ProcessMove;
            
            _inputActions.Moving.Disable();
            _inputActions.Interactions.Disable();
            _inputActions.UiEvents.Disable();
            
            _viewManager.CloseRootView();
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            _mainCamera.enabled = false;
            boardCamera.enabled = true;
            
            boardCamera.transform.position = _mainCamera.transform.position;
            boardCamera.transform.rotation = _mainCamera.transform.rotation;
            boardCamera.fieldOfView = _mainCamera.fieldOfView;
        }

        public void OnReturnToMainCamera()
        {
            State = BoardLobbyState.ProcessMove;
            
            mainBoard.BoardCollider.enabled = true;
            additionalBoard.BoardCollider.enabled = true;

            _viewManager.ShowView<InGamePresenter>();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            var sequence = DOTween.Sequence();

            sequence.Join(boardCamera.transform.DOMove(_mainCamera.transform.position, lerpTime))
                .Join(boardCamera.transform.DORotate(_mainCamera.transform.rotation.eulerAngles, lerpTime))
                .Join(boardCamera.DOFieldOfView(_mainCamera.fieldOfView, lerpTime));

            sequence.OnComplete(() =>
            {
                _mainCamera.enabled = true;
                boardCamera.enabled = false;
                State = BoardLobbyState.Unselected;
                
                _inputActions.Moving.Enable();
                _inputActions.Interactions.Enable();
                _inputActions.UiEvents.Enable();
            });
        }

        private void OnToAdditionalBoardMove()
        {
            State = BoardLobbyState.ProcessMove;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            var sequence = DOTween.Sequence();

            sequence.Join(boardCamera.transform.DOMove(additionalBoardCameraTransform.position, lerpTime))
                .Join(boardCamera.transform.DORotate(additionalBoardCameraTransform.rotation.eulerAngles, lerpTime));

            sequence.OnComplete(() =>
            {
                State = BoardLobbyState.AdditionalBoardSelected;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            });
        }
        
        private void OnToMainBoardMove()
        {
            State = BoardLobbyState.ProcessMove;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            var sequence = DOTween.Sequence();

            sequence.Join(boardCamera.transform.DOMove(mainBoardCameraTransform.position, lerpTime))
                .Join(boardCamera.transform.DORotate(mainBoardCameraTransform.rotation.eulerAngles, lerpTime));

            sequence.OnComplete(() =>
            {
                State = BoardLobbyState.MainBoardSelected;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            });
        }
    }
}