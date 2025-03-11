using System.Reflection;
using ElectrumGames.UI;

namespace ElectrumGames.Injection
{
    public class TutorialSelectorInstaller : BaseSceneInstaller
    {
        protected override Assembly UiAssembly => typeof(UiAssemblyReference).Assembly;
    }
}