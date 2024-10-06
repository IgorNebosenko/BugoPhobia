using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace ElectrumGames.UI.Components
{
    public class OnScreenInputButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button button;
        [Space]
        [InputControl(layout = "Button")] 
        [SerializeField] private string controlPath;

        protected override string controlPathInternal
        {
            get => controlPath; 
            set => controlPath = value;
        }

        private void SendEvent()
        {
            
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            button.onClick.AddListener(SendEvent);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            button.onClick.RemoveListener(SendEvent);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SendValueToControl(1f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SendValueToControl(0f);
        }
    }
}