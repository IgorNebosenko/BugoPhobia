using DG.Tweening;
using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Items
{
    public class ItemEmfElectronic : ItemInstanceBase, 
        IGhostHuntingHasElectricity, IGhostHuntingInteractableStay, IGhostHuntingInteractableExit
    {
        [Space]
        [SerializeField] private Material nonEmissionMaterial;
        [SerializeField] private Material emissionMaterial;
        [Space] 
        [SerializeField] private MeshRenderer[] emfMeshes;
        [Space]
        [SerializeField] private Transform arrowEmf;
        [SerializeField] private float arrowMoveDuration;
        [SerializeField] private float startAngle = -25f;
        [SerializeField] private float stepAngle = 12.5f;
        
        private bool _isOn;
        public bool IsElectricityOn => _isOn;
        
        public override void OnMainInteraction()
        {
            _isOn = !_isOn;
            
            if (_isOn)
                SetEmfLevel(0);
            else
                DisableEmf();
        }

        public override void OnAlternativeInteraction()
        {
        }

        public void SetEmfLevel(int level)
        {
            if (!_isOn || level < 0 || level >= emfMeshes.Length)
                return;
            

            for (var i = 0; i < emfMeshes.Length; i++)
            {
                emfMeshes[i].material = i <= level ? emissionMaterial : nonEmissionMaterial;
            }

            arrowEmf.DOLocalRotate(Vector3.forward * (startAngle + level * stepAngle), arrowMoveDuration);
        }

        private void DisableEmf()
        {
            for (var i = 0; i < emfMeshes.Length; i++)
            {
                emfMeshes[i].material = nonEmissionMaterial;
            }
            arrowEmf.DOLocalRotate(Vector3.forward * startAngle, arrowMoveDuration);
        }

        public void OnGhostInteractionStay()
        {
            SetEmfLevel(Random.Range(0, emfMeshes.Length - 1));
        }

        public void OnGhostInteractionExit()
        {
            SetEmfLevel(0);
        }

        public override void OnAfterDrop()
        {
            SetEmfLevel(0);
        }
    }
}