using ElectrumGames.Core.Items.Zones.Handlers;
using UnityEngine;

namespace ElectrumGames.Core.Tutorial
{
    public class TutorialUvHandlerInitializer : MonoBehaviour
    {
        [SerializeField] private UvPrintHandler uvPrintHandler;

        private void Start()
        {
            uvPrintHandler.MakeRandomPrint();
        }
    }
}