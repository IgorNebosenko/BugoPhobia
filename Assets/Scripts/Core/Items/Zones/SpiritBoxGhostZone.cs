using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Extensions;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Items.Zones
{
    public class SpiritBoxGhostZone : MonoBehaviour
    {
        private bool _hasEvidence;
        private bool _isMale;
        private bool _isYoung;
        
        private SpiritBoxConfig _spiritBoxConfig;

        [Inject] //for tutorial
        private void Construct(SpiritBoxConfig spiritBoxConfig)
        {
            _hasEvidence = true;
            _isMale = Random.Range(0, 2) != 0;
            _isYoung = Random.Range(0, 2) != 0;
            _spiritBoxConfig = spiritBoxConfig;
        }

        public void Init(bool hasRadioEvidence, bool isMale, bool isYoung, SpiritBoxConfig spiritBoxConfig)
        {
            _hasEvidence = hasRadioEvidence;
            _isMale = isMale;
            _isYoung = isYoung;
            
            _spiritBoxConfig = spiritBoxConfig;
        }

        public SpiritBoxDataElement GetResponse(SpiritBoxGhostRequest request)
        {
            if (!_hasEvidence || !IsOnLight())
                return SpiritBoxDataElement.Empty();
            
            switch (request)
            {
                case SpiritBoxGhostRequest.WhereGhost:
                    return _spiritBoxConfig.CloseDistanceVariants.PickRandom();
                case SpiritBoxGhostRequest.IsMale:
                    return _isMale ? _spiritBoxConfig.True : _spiritBoxConfig.False;
                case SpiritBoxGhostRequest.Age:
                    return _isYoung ? _spiritBoxConfig.Young : _spiritBoxConfig.Old;
                default:
                    return SpiritBoxDataElement.Empty();
            }
        }

        private bool IsOnLight()
        {
            return true;
        }
    }
}