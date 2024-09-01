using System.Reflection;
using ElectrumGames.Core.Player;
using ElectrumGames.UI;
using UnityEngine;

namespace ElectrumGames.Injection
{
    public class TestSceneInstaller : BaseSceneInstaller
    {
        [SerializeField] private PlayersFactory playersFactory;
        
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;

        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.BindInstance(playersFactory).AsSingle();
        }
    }
}