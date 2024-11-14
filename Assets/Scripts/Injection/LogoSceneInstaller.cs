using System.Reflection;
using ElectrumGames.UI;

namespace ElectrumGames.Injection
{
    public class LogoSceneInstaller : BaseSceneInstaller
    {
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;
    }
}