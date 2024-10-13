using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Controllers
{
    public class GhostModelController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        
        private static readonly int MovingSpeed = Animator.StringToHash("MovingSpeed");

        private void Update()
        {
            transform.forward = transform.parent.forward;
        }

        public void SetSpeed(float speed)
        {
            animator.SetFloat(MovingSpeed, speed);
        }

        public void SetVisibility(bool isOn)
        {
            if (!skinnedMeshRenderer.UnityNullCheck())
                skinnedMeshRenderer.enabled = isOn;
        }
    }
}