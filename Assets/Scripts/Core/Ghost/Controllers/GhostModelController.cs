using UnityEngine;

namespace ElectrumGames.Core.Ghost.Controllers
{
    public class GhostModelController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        
        private static readonly int MovingSpeed = Animator.StringToHash("MovingSpeed");

        public void SetSpeed(float speed)
        {
            animator.SetFloat(MovingSpeed, speed);
        }

        public void SetVisibility(bool isOn)
        {
            skinnedMeshRenderer.enabled = isOn;
        }
    }
}