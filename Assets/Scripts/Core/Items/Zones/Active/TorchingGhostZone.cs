using ElectrumGames.Core.Items.Equipment.Torchable;
using UnityEngine;

namespace ElectrumGames.Core.Items.Zones.Active
{
    public class TorchingGhostZone : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<TorchableBase>(out var torchable))
            {
                torchable.Torch();
            }
        }
    }
}