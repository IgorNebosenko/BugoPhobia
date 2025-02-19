using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.DebugElements.DebugElements
{
    public class CheatVariablesScreenHandler : MonoBehaviour
    {
        public void SetGhostType(int ghostType)
        {
            CheatVariables.SelectedGhostType = (GhostType)ghostType;
        }

        public void SetRoomId(int roomId)
        {
            CheatVariables.RoomId = roomId;
        }
    }
}