using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Player.Interactions
{
    public class LookAtGhostHandler
    {
        private Camera _targetCamera;

        private const int RayCastMask = ~(1 << 0 | 1 << 2);

        public LookAtGhostHandler(Camera targetCamera)
        {
            _targetCamera = targetCamera;
        }

        public IGhostCollider CheckIsLookAtGhost()
        {
            var ray = _targetCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

            if (Physics.Raycast(ray, out var hit, float.PositiveInfinity, RayCastMask))
            {
                if (hit.collider.TryGetComponent<IGhostCollider>(out var ghostCollider))
                {
                    return ghostCollider;
                }
            }

            return null;
        }
    }
}