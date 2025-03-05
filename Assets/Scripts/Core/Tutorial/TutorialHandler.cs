using UnityEngine;

namespace ElectrumGames.Core.Tutorial
{
    public class TutorialHandler
    {
        private const string TutorialStatusKey = "TutorialStatus";

        public bool IsTutorialFinished
        {
            get => PlayerPrefs.GetInt(TutorialStatusKey, 0) != 0;
            set => PlayerPrefs.SetInt(TutorialStatusKey, value ? 1 : 0);
        }
    }
}