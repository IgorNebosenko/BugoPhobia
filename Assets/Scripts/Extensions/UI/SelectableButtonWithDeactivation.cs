using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ElectrumGames.Extensions.UI
{
    public class SelectableButtonWithDeactivation : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private Button selectableButton;
        [SerializeField] private Image buttonImage;

        [SerializeField] private Sprite deactivateState;
        [SerializeField] private Sprite pointerDownSprite;
        [SerializeField] private Sprite pointerUpSprite;

        private bool _lastActiveState;

        private void Start()
        {
            buttonImage.sprite = deactivateState;
        }

        private void Update()
        {
            if (_lastActiveState == selectableButton.interactable)
                return;

            _lastActiveState = !_lastActiveState;

            if (_lastActiveState)
                buttonImage.sprite = pointerUpSprite;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_lastActiveState)
                return;

            buttonImage.sprite = pointerDownSprite;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_lastActiveState)
                return;

            buttonImage.sprite = pointerUpSprite;
        }
    }
}