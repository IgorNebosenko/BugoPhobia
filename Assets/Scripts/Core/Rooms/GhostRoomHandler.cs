using System.Collections.Generic;
using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class GhostRoomHandler : MonoBehaviour
    {
        [SerializeField] private Transform[] activityPoints;
        [SerializeField] private Transform[] sheltersPoints;

        public IReadOnlyList<Transform> ActivityPoints => activityPoints;
        public IReadOnlyList<Transform> SheltersPoints => sheltersPoints;
    }
}