using System.Collections.Generic;
using UnityEngine;

namespace ElectrumGames.Core.Rooms
{
    public class HuntPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] huntPoints;

        public IReadOnlyList<Transform> Positions => huntPoints;
    }
}