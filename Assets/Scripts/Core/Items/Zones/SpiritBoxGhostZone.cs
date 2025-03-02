using System;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Items.Zones
{
    public class SpiritBoxGhostZone : MonoBehaviour
    {
        private bool _hasEvidence;
        private bool _isMale;
        private bool _isYoung;
        
        private RadioConfig _radioConfig;
        
        public void Init(bool hasRadioEvidence, bool isMale, bool isYoung, RadioConfig radioConfig)
        {
            _hasEvidence = hasRadioEvidence;
            _isMale = isMale;
            _isYoung = isYoung;
            
            _radioConfig = radioConfig;
        }

        public RadioDataElement GetResponse(SpiritBoxGhostRequest request)
        {
            switch (request)
            {
                case SpiritBoxGhostRequest.WhereGhost:
                    return _radioConfig.CloseDistanceVariants.PickRandom();
                case SpiritBoxGhostRequest.IsMale:
                    return _isMale ? _radioConfig.True : _radioConfig.False;
                case SpiritBoxGhostRequest.Age:
                    return _isYoung ? _radioConfig.Young : _radioConfig.Old;
                default:
                    return new RadioDataElement();
            }
        }
    }
}