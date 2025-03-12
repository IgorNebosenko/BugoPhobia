using System.Reflection;
using ElectrumGames.Audio;
using ElectrumGames.Audio.Pool;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Environment.House;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Player.Interactions.Items;
using ElectrumGames.UI;
using ElectrumGames.UI.UiEvents;
using UnityEngine;

namespace ElectrumGames.Injection
{
    public class TutorialInstaller : BaseSceneInstaller
    {
        [Space]
        [SerializeField] private Camera mainCamera;
        [Space]
        [SerializeField] private PlayersFactory playersFactory;
        [SerializeField] private ItemsFactory itemsFactory;
        [SerializeField] private AudioSourcesPool audioSourcesPool;
        [Space]
        [SerializeField] private ItemMarkers itemMarkers;
        [Space]
        [SerializeField] private FuseBoxEnvironmentObject fuseBoxEnvironmentObject;
        [Space]
        [SerializeField] private GhostEmfZonePool ghostEmfZonePool;
        [Space]
        [SerializeField] private TutorialHouseController tutorialHouseController;
        [SerializeField] private EnvironmentHandler environmentHandler;
        [SerializeField] private NoiseGenerator noiseGenerator;
        [Space]
        [SerializeField] private FlashLightInteractionHandler flashLightInteractionHandler;
        
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;

        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.BindInstance(mainCamera).AsSingle();

            Container.BindInstance(playersFactory).AsSingle();
            Container.BindInstance(itemsFactory).AsSingle();
            Container.BindInstance(audioSourcesPool).AsSingle();

            Container.BindInstance(itemMarkers).AsSingle();

            Container.BindInstance(fuseBoxEnvironmentObject).AsSingle();

            Container.BindInstance(ghostEmfZonePool).AsSingle();

            Container.BindInstance(tutorialHouseController).AsSingle();
            Container.BindInstance(environmentHandler).AsSingle();
            Container.BindInstance(noiseGenerator).AsSingle();

            Container.BindInstance(flashLightInteractionHandler).AsSingle();
            
            Container.Bind<UiEventsHandler>().AsSingle().NonLazy();
            
            Container.Bind<MissionPlayersHandler>().AsSingle();
        }
    }
}