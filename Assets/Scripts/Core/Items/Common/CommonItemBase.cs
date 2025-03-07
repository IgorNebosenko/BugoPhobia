using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class CommonItemBase : ItemInstanceBase, IGhostThrowable
    {
        public Transform Transform => transform;
        
        public override void OnMainInteraction()
        {
        }

        public override void OnAlternativeInteraction()
        {
        }

        public void ThrowItem(float force)
        {
            var direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            isInDropState = true;
            
            PhysicObject.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}