using ElectrumGames.Configs;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Player.Movement;
using UnityEngine;

namespace ElectrumGames.Core.Player.Interactions
{
    public class ExternalInteractionManager : IInteractionItemsManager
    {
        private const int RayCastIgnoreMask = ~(1 << 2);
        
        private readonly IInteraction _interactions;
        private readonly IInput _input;
        private readonly Camera _targetCamera;
        private readonly PlayerConfig _playerConfig;

        private bool _lastState;

        public ExternalInteractionManager(IInteraction interactions, IInput input, Camera targetCamera,
            PlayerConfig playerConfig)
        {
            _interactions = interactions;
            _input = input;
            _targetCamera = targetCamera;
            _playerConfig = playerConfig;
        }

        public void Simulate(float deltaTime)
        {
            if (_interactions.ExternalInteraction != _lastState)
            {
                _lastState = !_lastState;

                if (_lastState)
                {
                    var ray = _targetCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

                    if (Physics.Raycast(ray, out var hit, _playerConfig.RayCastDistance, RayCastIgnoreMask))
                    {
                        if (hit.collider.TryGetComponent<EnvironmentObjectBase>(out var environmentObject))
                        {
                            environmentObject.OnInteract();
                        }
                    }
                }
            }

            if (_interactions.ExternalInteraction)
            {
                var ray = _targetCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

                if (Physics.Raycast(ray, out var hit, _playerConfig.RayCastDistance, RayCastIgnoreMask))
                {
                    if (hit.collider.TryGetComponent<DoorEnvironmentObject>(out var door))
                    {
                        door.SetCameraAngleAndInteract(_input.Look);
                    }
                }
            }
        }
    }
}