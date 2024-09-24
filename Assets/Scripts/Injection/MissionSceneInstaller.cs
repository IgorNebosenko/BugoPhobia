using System.Reflection;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Player;
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
        [SerializeField] private PlayersFactory playersFactory;
        [SerializeField] private ItemsFactory itemsFactory;
        [Space]
        [SerializeField] private Camera injectedCamera;
        [Space]
        [SerializeField] private ItemMarkers itemMarkers;
        
        
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;

        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.BindInstance(environmentHandler).AsSingle();

            Container.BindInstance(playersFactory).AsSingle();
            Container.BindInstance(itemsFactory).AsSingle();

            Container.BindInstance(injectedCamera).AsSingle();

            Container.BindInstance(itemMarkers).AsSingle();
            
            Container.Bind<EnvironmentData>().AsSingle();
            
            Container.Bind<UiEventsHandler>().AsSingle().NonLazy();
        }
    }
}