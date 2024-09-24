using System;
using UnityEngine.Events;

namespace ElectrumGames.Core.Environment
{
    public class HouseKeyEnvironmentObject : EnvironmentObjectBase
    {
        public static event Action PickUpKey;
        public override void OnInteract()
        {
            PickUpKey?.Invoke();
            Destroy(gameObject);
        }
    }
}