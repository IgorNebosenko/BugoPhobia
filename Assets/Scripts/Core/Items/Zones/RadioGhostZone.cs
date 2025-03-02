using System;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Items.Zones
{
    public class RadioGhostZone : MonoBehaviour
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

        public RadioDataElement GetResponse(RadioGhostRequest request)
        {
            switch (request)
            {
                case RadioGhostRequest.WhereGhost:
                    return _radioConfig.CloseDistanceVariants.PickRandom();
                case RadioGhostRequest.IsMale:
                    return _isMale ? _radioConfig.True : _radioConfig.False;
                case RadioGhostRequest.Age:
                    return _isYoung ? _radioConfig.Young : _radioConfig.Old;
                default:
                    return new RadioDataElement();
            }
        }
    }
}