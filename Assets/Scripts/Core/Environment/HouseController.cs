using ElectrumGames.Core.Environment.Enums;
using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class HouseController : MonoBehaviour
    {
        [SerializeField] private FuseBoxEnvironmentObject fuseBox;
        
        public SwitchState SwitchState { get; private set; }
        
        public bool IsKeyPicked { get; private set; }
        
        public bool IsEnterDoorOpened { get; set; }

        public void OnSwitchStateChanged(bool state)
        {
            if (SwitchState == SwitchState.Broken)
                return;

            SwitchState = state ? SwitchState.Enabled : SwitchState.Disabled;
        }

        public void OnPickUpKey()
        {
            IsKeyPicked = true;
        }

        private void OnEnable()
        {
            fuseBox.SwitchChanged += OnSwitchStateChanged;
            HouseKeyEnvironmentObject.PickUpKey += OnPickUpKey;
        }

        private void OnDisable()
        {
            fuseBox.SwitchChanged -= OnSwitchStateChanged;
            HouseKeyEnvironmentObject.PickUpKey -= OnPickUpKey;
        }
    }
}