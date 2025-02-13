using System.Reflection;
using ElectrumGames.Core.Player.Interactions.Items;
using ElectrumGames.Audio;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Environment.House;
using ElectrumGames.Core.Ghost;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using ElectrumGames.UI;
using ElectrumGames.UI.UiEvents;
using UnityEngine;

namespace ElectrumGames.Injection
{
    public class MissionSceneInstaller : BaseSceneInstaller
    {
        [Space]
        [SerializeField] private EnvironmentHandler environmentHandler;
        [Space]
        [SerializeField] private GhostEmfZonePool ghostEmfZonePool;
        [Space]
        [SerializeField] private PlayersFactory playersFactory;
        [SerializeField] private GhostFactory ghostFactory;
        [SerializeField] private ItemsFactory itemsFactory;
        [Space]
        [SerializeField] private HouseController houseController;
        [SerializeField] private FuseBoxEnvironmentObject fuseBoxEnvironmentObject;
        [Space]
        [SerializeField] private HuntPoints huntPoints;
        [Space]
        [SerializeField] private Camera injectedCamera;
        [Space]
        [SerializeField] private ItemMarkers itemMarkers;
        [Space]
        [SerializeField] private NoiseGenerator noiseGenerator;
        [Space]
        [SerializeField] private FlashLightInteractionHandler flashLightInteractionHandler;
        
        
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;

        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.BindInstance(environmentHandler).AsSingle();

            Container.BindInstance(ghostEmfZonePool).AsSingle();

            Container.BindInstance(playersFactory).AsSingle();
            Container.BindInstance(ghostFactory).AsSingle();
            Container.BindInstance(itemsFactory).AsSingle();
            
            Container.BindInstance(houseController).AsSingle();
            
            Container.Bind<IFuseBoxInteractable>().FromInstance(fuseBoxEnvironmentObject).AsSingle();
            Container.BindInstance(fuseBoxEnvironmentObject).AsSingle();

            Container.BindInstance(huntPoints).AsSingle();

            Container.BindInstance(injectedCamera).AsSingle();

            Container.BindInstance(itemMarkers).AsSingle();

            Container.BindInstance(noiseGenerator).AsSingle();

            Container.BindInstance(flashLightInteractionHandler).AsSingle();
            
            Container.Bind<EnvironmentData>().AsSingle();

            Container.Bind<EvidenceController>().AsSingle();

            Container.Bind<MissionPlayersHandler>().AsSingle();
            
            Container.Bind<UiEventsHandler>().AsSingle().NonLazy();
        }
    }
}