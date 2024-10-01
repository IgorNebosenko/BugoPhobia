using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class CommonItemBase : ItemInstanceBase, IGhostThrowable
    {
        public override void OnMainInteraction()
        {
        }

        public override void OnAlternativeInteraction()
        {
        }

        public void ThrowItem(float force)
        {
            var direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            
            Debug.Log($"TryThrowItem on {direction * force}");
            PhysicObject.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}