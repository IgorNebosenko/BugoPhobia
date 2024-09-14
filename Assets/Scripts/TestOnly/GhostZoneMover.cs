using DG.Tweening;
using UnityEngine;

namespace ElectrumGames
{
    public class GhostZoneMover : MonoBehaviour
    {
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private float timeSpeed;
        
        private void Start()
        {
            transform.DOMove(targetPosition, timeSpeed).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
