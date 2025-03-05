using ElectrumGames.Core.Tutorial;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using Zenject;

namespace ElectrumGames.EntryPoints
{
    public class SceneSelectorEntry : MonoBehaviour
    {
        private TutorialHandler _tutorialHandler;
        private ViewManager _viewManager;
        
        [Inject]
        private void Construct(TutorialHandler tutorialHandler, ViewManager viewManager)
        {
            _tutorialHandler = tutorialHandler;
            _viewManager = viewManager;
        }

        private void Start()
        {
            if (!_tutorialHandler.IsTutorialFinished)
                _viewManager.ShowView<SelectTutorialViewPresenter>();
        }
    }
}