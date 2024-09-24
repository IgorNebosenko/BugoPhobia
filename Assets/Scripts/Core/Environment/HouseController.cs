using ElectrumGames.Core.Environment.Enums;
using ElectrumGames.Core.Ghost;
using ElectrumGames.GlobalEnums;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Environment
{
    public class HouseController : MonoBehaviour
    {
        [SerializeField] private FuseBoxEnvironmentObject fuseBox;
        [Space]
        [SerializeField] private int minGhostId = 0;
        [SerializeField] private int maxGhostId = 19;
        [Space]
        [SerializeField] private int minRoomId = 0;
        [SerializeField] private int maxRoomId = 0;
        
        public SwitchState SwitchState { get; private set; }
        
        public bool IsKeyPicked { get; private set; }
        
        public bool IsEnterDoorOpened { get; set; }

        [Inject]
        private void Construct(GhostEnvironmentHandler ghostEnvironmentHandler)
        {
            ghostEnvironmentHandler.InitGhost(minGhostId, maxGhostId, minRoomId, maxRoomId);
        }

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