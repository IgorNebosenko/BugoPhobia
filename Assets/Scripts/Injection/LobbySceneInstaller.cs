using System.Reflection;
using ElectrumGames.Core.Player.Interactions.Items;
using ElectrumGames.Audio;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Player;
using ElectrumGames.UI;
using ElectrumGames.UI.UiEvents;
using UnityEngine;

namespace ElectrumGames. Injection
{
    public class LobbySceneInstaller : BaseSceneInstaller
    {
        [Space]
        [SerializeField] private PlayersFactory playersFactory;
        [SerializeField] private ItemsFactory itemsFactory;
        [Space]
        [SerializeField] private NoiseGenerator noiseGenerator;
        [SerializeField] private EnvironmentHandler lobbyEnvironmentHandler;
        [Space]
        [SerializeField] private Camera injectedCamera;
        [Space]
        [SerializeField] private ItemMarkers itemMarkers;
        [Space]
        [SerializeField] private FlashLightInteractionHandler flashLightInteractionHandler;
        
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;

        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.BindInstance(playersFactory).AsSingle();
            Container.BindInstance(itemsFactory).AsSingle();

            Container.BindInstance(noiseGenerator).AsSingle();
            Container.BindInstance(lobbyEnvironmentHandler).AsSingle();

            Container.BindInstance(injectedCamera).AsSingle();

            Container.BindInstance(itemMarkers).AsSingle();

            Container.BindInstance(flashLightInteractionHandler).AsSingle();
            
            Container.Bind<UiEventsHandler>().AsSingle().NonLazy();
        }
    }
}
