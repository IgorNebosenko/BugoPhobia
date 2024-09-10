using ElectrumGames.Configs;
using ElectrumGames.Core.Items;
using UnityEngine;

namespace Core.Player.Interactions
{
    public class ExternalInteractionHandler
    {
        private readonly IInteraction _interactions;
        private readonly Camera _targetCamera;
        private readonly PlayerConfig _playerConfig;

        private bool _previousState;
        
        public ExternalInteractionHandler(IInteraction interactions, Camera targetCamera, PlayerConfig playerConfig)
        {
            _interactions = interactions;
            _targetCamera = targetCamera;
            _playerConfig = playerConfig;
        }

        public void Simulate(float deltaTime)
        {
            if (_previousState != _interactions.PutInteraction)
            {
                _previousState = !_previousState;

                if (_previousState)
                {
                    var ray = _targetCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

                    if (Physics.Raycast(ray, out var hit, _playerConfig.RayCastDistance))
                    {
                        if (hit.collider.TryGetComponent<ItemInstanceBase>(out var item))
                        {
                            Debug.Log($"Hit into {item.ItemType}");
                        }
                    }
                    //Todo realize for switches
                    //Todo realize for doors
                }
            }
        }
    }
}