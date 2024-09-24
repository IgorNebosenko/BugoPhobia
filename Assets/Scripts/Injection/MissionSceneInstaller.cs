using System.Reflection;
using ElectrumGames.Core.Environment;
using ElectrumGames.UI;
using UnityEngine;

namespace ElectrumGames.Injection
{
    public class MissionSceneInstaller : BaseSceneInstaller
    {
        [Space]
        [SerializeField] private EnvironmentHandler environmentHandler;
        
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;

        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.BindInstance(environmentHandler).AsSingle();

            Container.Bind<EnvironmentData>().AsSingle();
        }
    }
}