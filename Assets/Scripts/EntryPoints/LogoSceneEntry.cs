using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using Zenject;

namespace ElectrumGames.EntryPoints
{
    public class LogoSceneEntry : MonoBehaviour
    {
        private ViewManager _viewManager;
        
        [Inject]
        private void Construct(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            Application.runInBackground = true;
            
            _viewManager.ShowView<WarningPresenter>();
        }
    }
}