using ElectrumGames.Core.Environment;
using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class LightRoomHandler : MonoBehaviour
    {
        [SerializeField] private SwitchEnvironmentObject roomSwitch;
        [SerializeField] private LightEnvironmentObject[] lightEnvironmentObjects;
        [SerializeField] private LightEnvironmentObject[] switchableLamps;

        private void OnEnable()
        {
            roomSwitch.Switch += ChangeState;
        }

        private void ChangeState(bool state, bool includeSwitchableLamps = false)
        {
            for (var i = 0; i < lightEnvironmentObjects.Length; i++)
            {
                lightEnvironmentObjects[i].SwitchStateTo(state);
            }

            if (includeSwitchableLamps)
            {
                for (var i = 0; i < switchableLamps.Length; i++)
                    switchableLamps[i].SwitchStateTo(state);
            }
        }
    }
}