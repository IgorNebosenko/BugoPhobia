using DG.Tweening;
using ElectrumGames.Configs;
using ElectrumGames.Core.Player.Movement;
using UnityEngine;

namespace ElectrumGames.Core.PlayerVisuals
{
    public class HeadBobVisual : IInputSimulateVisuals
    {
        private Camera _camera;
        private ConfigService _configService;
        private PlayerConfig _playerConfig;
        
        private Vector3 _defaultPos;
        private Tween _bobTween;
        
        private bool _isInited;
        
        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        public void Init(ConfigService configService, PlayerConfig playerConfig)
        {
            _configService = configService;
            _playerConfig = playerConfig;
            
            if (!_configService.EnableHeadBob)
                return;
            
            _defaultPos = _camera.transform.localPosition;

            _isInited = true;
        }

        public void Simulate(IInput input, float deltaTime)
        {
            if (!_isInited)
                return;

            if (input.Movement != Vector2.zero)
            {
                if (_bobTween == null || !_bobTween.IsPlaying())
                    StartHeadBob();
            }
            else
            {
                if (_bobTween != null)
                    StopHeadBob();
            }
        }

        private void StartHeadBob()
        {
            _bobTween = _camera.transform.DOShakePosition(_playerConfig.BobSpeed, 
                Vector3.up * _playerConfig.BobAmount, 1, 90, false, true).
                SetLoops(-1, LoopType.Yoyo);
        }

        private void StopHeadBob()
        {
            _bobTween.Kill();
            _camera.transform.localPosition = _defaultPos;
        }
    }
}