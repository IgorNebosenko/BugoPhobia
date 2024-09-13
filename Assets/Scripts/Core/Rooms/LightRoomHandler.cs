using ElectrumGames.Core.Environment;
using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class LightRoomHandler : MonoBehaviour
    {
        [SerializeField] private SwitchEnvironmentObject roomSwitch;
        [SerializeField] private LightEnvironmentObject[] lightEnvironmentObjects;

        private void OnEnable()
        {
            roomSwitch.Switch += ChangeState;
        }

        private void ChangeState(bool state)
        {
            for (var i = 0; i < lightEnvironmentObjects.Length; i++)
            {
                lightEnvironmentObjects[i].SwitchStateTo(state);
            }
        }
    }
}