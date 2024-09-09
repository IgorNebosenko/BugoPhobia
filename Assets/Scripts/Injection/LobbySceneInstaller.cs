using System.Reflection;
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
        [SerializeField] private Camera injectedCamera;
        
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;

        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.BindInstance(playersFactory).AsSingle();
            Container.BindInstance(itemsFactory).AsSingle();

            Container.BindInstance(injectedCamera).AsSingle();
            
            Container.Bind<UiEventsHandler>().AsSingle().NonLazy();
        }
    }
}
