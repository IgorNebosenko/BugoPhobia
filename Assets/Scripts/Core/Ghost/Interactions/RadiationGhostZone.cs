using System.Collections;
using System.Linq;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Missions;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class RadiationGhostZone : MonoBehaviour
    {
        private EvidenceController _evidenceController;
        private RadiationConfig _radiationConfig;

        public float CurrentRadiation { get; private set; }
        
        public void Init(EvidenceController evidenceController, RadiationConfig radiationConfig)
        {
            _evidenceController = evidenceController;
            _radiationConfig = radiationConfig;
            
            CurrentRadiation = _radiationConfig.StartRadiation;
            
            var radiationAccumulationTime = Random.Range(_radiationConfig.MinAccumulationTime, 
                _radiationConfig.MaxAccumulationTime);

            float radiationTargetValue;

            if (evidenceController.Evidences.Contains(EvidenceType.Radiation))
            {
                radiationTargetValue = Random.Range(_radiationConfig.RadiationValueEvidence,
                    _radiationConfig.RadiationValueEvidenceMax);
            }
            else
            {
                radiationTargetValue = Random.Range(_radiationConfig.RadiationValueMedium,
                    _radiationConfig.RadiationValueEvidence - _radiationConfig.DifferenceRadiation);
            }
            
            StartCoroutine(AccumulateRadiation(radiationAccumulationTime, radiationTargetValue));
        }

        private IEnumerator AccumulateRadiation(float time, float targetValue)
        {
            Debug.Log($"Accumulating radiation | Time: {time} | Target value: {targetValue}");
            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                
                var percent = elapsedTime / time;
                CurrentRadiation = Mathf.Lerp(CurrentRadiation, targetValue, percent);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}