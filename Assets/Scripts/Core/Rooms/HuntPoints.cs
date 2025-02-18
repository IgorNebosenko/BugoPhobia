using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class HuntPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] huntPoints;
        [SerializeField] private Transform[] shelters;

        public IReadOnlyList<Transform> Positions => huntPoints;
        public IReadOnlyList<Vector3> Shelters => shelters.Select(x => x.position).ToList();
    }
}