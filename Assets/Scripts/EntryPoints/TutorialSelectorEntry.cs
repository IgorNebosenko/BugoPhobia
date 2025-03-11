using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using Zenject;

namespace ElectrumGames.EntryPoints
{
    public class TutorialSelectorEntry : MonoBehaviour
    {
        [Inject]
        private void Construct(ViewManager viewManager)
        {
            viewManager.ShowView<SelectTutorialViewPresenter>();
        }
    }
}