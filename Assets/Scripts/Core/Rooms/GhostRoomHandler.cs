using System.Collections.Generic;
using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class GhostRoomHandler : MonoBehaviour
    {
        [SerializeField] private Transform spawnPointAtRoom;
        [Space]
        [SerializeField] private Transform[] activityPoints;
        [SerializeField] private Transform[] sheltersPoints;

        public Transform SpawnPointAtRoom => spawnPointAtRoom;
        public IReadOnlyList<Transform> ActivityPoints => activityPoints;
        public IReadOnlyList<Transform> SheltersPoints => sheltersPoints;
    }
}