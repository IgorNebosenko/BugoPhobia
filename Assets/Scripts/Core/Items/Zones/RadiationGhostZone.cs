using System.Collections;
using ElectrumGames.Core.Ghost.Configs;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items.Zones
{
    public class RadiationGhostZone : MonoBehaviour
    {
        public float CurrentRadiation { get; private set; }

        [Inject] //for tutorial
        private void Construct(RadiationConfig radiationConfig)
        {
            CurrentRadiation = Random.Range(radiationConfig.RadiationValueEvidence,
                radiationConfig.RadiationValueEvidenceMax);
        }

        public void Init(bool hasEvidence, RadiationConfig radiationConfig)
        {
            CurrentRadiation = radiationConfig.StartRadiation;
            
            var radiationAccumulationTime = Random.Range(radiationConfig.MinAccumulationTime, 
                radiationConfig.MaxAccumulationTime);

            float radiationTargetValue;

            if (hasEvidence)
            {
                radiationTargetValue = Random.Range(radiationConfig.RadiationValueEvidence,
                    radiationConfig.RadiationValueEvidenceMax);
            }
            else
            {
                radiationTargetValue = Random.Range(radiationConfig.RadiationValueMedium,
                    radiationConfig.RadiationValueEvidence - radiationConfig.DifferenceRadiation);
            }
            
            StartCoroutine(AccumulateRadiation(radiationAccumulationTime, radiationTargetValue));
        }

        private IEnumerator AccumulateRadiation(float time, float targetValue)
        {
            var elapsedTime = 0f;
            
            var startRadiation = CurrentRadiation;

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                
                var percent = elapsedTime / time;
                CurrentRadiation = Mathf.Lerp(startRadiation, targetValue, percent);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}