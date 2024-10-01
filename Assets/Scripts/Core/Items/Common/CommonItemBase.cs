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

        public void ThrowItem(Vector3 direction, float force)
        {
            PhysicObject.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}