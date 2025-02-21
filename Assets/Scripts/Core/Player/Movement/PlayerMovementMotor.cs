using ElectrumGames.Configs;
using UnityEngine;

namespace ElectrumGames.Core.Player.Movement
{
    public class PlayerMovementMotor : IMotor
    {
        private float _xRotation;
        private float _gravityVelocity;

        private float MaxSprintDuration => _playerConfig.RunDuration;
        public bool CanSprint => _sprintDuration > 0;
        
        private float _sprintDuration;
        private float _sprintCooldown;
        
        private readonly CharacterController _characterController;
        private readonly Camera _camera;
        private readonly PlayerConfig _playerConfig;
        private readonly ConfigService _configService;
        
        public PlayerMovementMotor(CharacterController characterController, Camera playerCamera, 
            PlayerConfig playerConfig, ConfigService configService)
        {
            _characterController = characterController;
            _camera = playerCamera;
            _playerConfig = playerConfig;
            _configService = configService;

            _sprintDuration = MaxSprintDuration;
        }

        public void Simulate(IInput input, float deltaTime)
        {
            #region Rotation
            var transform = _characterController.transform;
            var xSensitivity = _configService.XSensitivity * (_configService.EnableXInversion ? -1 : 1);
            var ySensitivity = _configService.YSensitivity * (_configService.EnableYInversion ? -1 : 1);
            
            var cameraRotation = new Vector2(
                input.Look.x * xSensitivity * Time.fixedDeltaTime, 
                input.Look.y * ySensitivity * Time.fixedDeltaTime);

            _xRotation -= cameraRotation.y;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y + cameraRotation.x, 0f);
            _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            #endregion
        }

        public void FixedSimulate(IInput input, float deltaTime)
        {
            #region Moving
            var transform = _characterController.transform;
            var move = transform.right * input.Movement.x + transform.forward * input.Movement.y;

            if (_sprintCooldown > 0)
            {
                _sprintCooldown -= deltaTime;
                if (_sprintCooldown <= 0f)
                    _sprintDuration = MaxSprintDuration;
            }

            if (input.Sprint && CanSprint && input.Movement != Vector2.zero)
            {
                _sprintDuration -= deltaTime;
                
                _sprintCooldown = _playerConfig.RunCooldown;
                
                _characterController.Move(move * (_playerConfig.RunSpeed * deltaTime));
            }
            else
                _characterController.Move(move * (_playerConfig.DefaultSpeed * deltaTime));
            
            //Gravity task
            if (_characterController.isGrounded)
                _gravityVelocity = 0f;
            else
                _gravityVelocity += Physics.gravity.y * deltaTime;
            
            _characterController.Move(Vector3.up * (_gravityVelocity * deltaTime));

            move.y = _gravityVelocity;
            #endregion
        }
    }
}