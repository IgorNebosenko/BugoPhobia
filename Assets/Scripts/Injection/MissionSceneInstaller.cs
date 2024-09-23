using System.Reflection;
using ElectrumGames.Core.Environment;
using ElectrumGames.UI;

namespace ElectrumGames.Injection
{
    public class MissionSceneInstaller : BaseSceneInstaller
    {
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;

        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.Bind<EnvironmentData>().AsSingle();
        }
    }
}