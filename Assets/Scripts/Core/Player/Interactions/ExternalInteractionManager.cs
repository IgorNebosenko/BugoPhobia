using ElectrumGames.Configs;
using ElectrumGames.Core.Environment;
using UnityEngine;

namespace Core.Player.Interactions
{
    public class ExternalInteractionManager : IInteractionItemsManager
    {
        private readonly IInteraction _interactions;
        private readonly Camera _targetCamera;
        private readonly PlayerConfig _playerConfig;

        private bool _lastState;

        public ExternalInteractionManager(IInteraction interactions, Camera targetCamera, PlayerConfig playerConfig)
        {
            _interactions = interactions;
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

                    if (Physics.Raycast(ray, out var hit, _playerConfig.RayCastDistance))
                    {
                        if (hit.collider.TryGetComponent<EnvironmentObjectBase>(out var environmentObject))
                        {
                            environmentObject.OnInteract();
                        }
                    }
                }
            }
        }
    }
}