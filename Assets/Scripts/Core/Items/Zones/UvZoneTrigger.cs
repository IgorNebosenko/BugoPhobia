using ElectrumGames.Core.Items.Zones.Handlers;
using UnityEngine;

namespace ElectrumGames.Core.Items.Zones
{
    public class UvZoneTrigger : MonoBehaviour
    {
        [SerializeField] private float forceUv = 0.5f;

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<UvPrintHandler>(out var uvPrintHandler))
            {
                uvPrintHandler.ChargingProcess(Time.deltaTime * forceUv);
            }
        }
    }
}