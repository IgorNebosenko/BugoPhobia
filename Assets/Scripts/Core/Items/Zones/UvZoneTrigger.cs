using ElectrumGames.Core.Items.Zones.Handlers;
using UnityEngine;

namespace ElectrumGames.Core.Items.Zones
{
    public class UvZoneTrigger : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<UvPrintHandler>(out var uvPrintHandler))
            {
                
            }
        }
    }
}