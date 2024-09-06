using DG.Tweening;
using ElectrumGames.Configs;
using UnityEngine;

namespace ElectrumGames.Core.Player.Movement
{
    public class CameraLifter
    {
        private readonly PlayerConfig _playerConfig;
        
        private readonly Transform _headBob;
        
        private readonly Vector3 _stayPos;
        private readonly Vector3 _sitPos;

        public bool IsStay { get; private set; } = true;

        private bool _lastState;
        private bool _lockState;
        
        private Tween _tween;
        
        public CameraLifter(PlayerConfig playerConfig,Transform headBob, Vector3 stayPos, Vector3 sitPos)
        {
            _playerConfig = playerConfig;
            
            _headBob = headBob;
            
            _stayPos = stayPos;
            _sitPos = sitPos;
        }

        public void UpdateInput(IInput input)
        {
            if (input.IsCrouch && !_lastState && !_lockState)
            {
                _lastState = true;
                _lockState = true;
                
                IsStay = !IsStay;

                _tween?.Kill();
                _tween = _headBob.DOLocalMove(IsStay ? _stayPos : _sitPos, _playerConfig.SitStandDuration).
                    OnComplete(() => _lockState = false);
            }
            else if (!input.IsCrouch)
            {
                _lastState = false;
            }
        }
    }
}