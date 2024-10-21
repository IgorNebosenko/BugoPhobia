using DG.Tweening;
using ElectrumGames.Audio;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Ghost.Configs;
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
        [Space] 
        [SerializeField] private EmfData emfData;
        [SerializeField] private ToneGenerator toneGenerator;
        
        private bool _isOn;
        private int _lastEmfLevel;
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
            
            if  (_lastEmfLevel > level && level != 0)
                return;

            for (var i = 0; i < emfMeshes.Length; i++)
            {
                emfMeshes[i].material = i <= level ? emissionMaterial : nonEmissionMaterial;
            }

            if (_lastEmfLevel != level)
            {
                switch (level)
                {
                    case 0:
                        toneGenerator.Stop();
                        break;
                    case 1:
                        toneGenerator.GenerateSound(emfData.EmfWaveForm, emfData.Level2Tone, emfData.EmfVolume);
                        break;
                    case 2:
                        toneGenerator.GenerateSound(emfData.EmfWaveForm, emfData.Level3Tone, emfData.EmfVolume);
                        break;
                    case 3:
                        toneGenerator.GenerateSound(emfData.EmfWaveForm,emfData.Level4Tone, emfData.EmfVolume);
                        break;
                    case 4:
                        toneGenerator.GenerateSound(emfData.EmfWaveForm, emfData.Level5Tone, emfData.EmfVolume);
                        break;
                    default:
                        toneGenerator.Stop();
                        break;
                }
            }

            _lastEmfLevel = level;

            arrowEmf.DOLocalRotate(Vector3.forward * (startAngle + level * stepAngle), arrowMoveDuration);
        }

        private void DisableEmf()
        {
            for (var i = 0; i < emfMeshes.Length; i++)
            {
                emfMeshes[i].material = nonEmissionMaterial;
            }
            arrowEmf.DOLocalRotate(Vector3.forward * startAngle, arrowMoveDuration);
            
            toneGenerator.Stop();
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