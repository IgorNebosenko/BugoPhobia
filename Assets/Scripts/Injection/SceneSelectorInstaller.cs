using System.Reflection;
using ElectrumGames.UI;

namespace ElectrumGames.Injection
{
    public class SceneSelectorInstaller : BaseSceneInstaller
    {
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;
    }
}