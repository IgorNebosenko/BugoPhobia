using System;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Extensions;
using UnityEngine;

namespace ElectrumGames.Core.Items.Zones
{
    public class SpiritBoxGhostZone : MonoBehaviour
    {
        private bool _hasEvidence;
        private bool _isMale;
        private bool _isYoung;
        
        private SpiritBoxConfig _spiritBoxConfig;
        
        public void Init(bool hasRadioEvidence, bool isMale, bool isYoung, SpiritBoxConfig spiritBoxConfig)
        {
            _hasEvidence = hasRadioEvidence;
            _isMale = isMale;
            _isYoung = isYoung;
            
            _spiritBoxConfig = spiritBoxConfig;
        }

        public SpiritBoxDataElement GetResponse(SpiritBoxGhostRequest request)
        {
            if (!_hasEvidence)
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
    }
}