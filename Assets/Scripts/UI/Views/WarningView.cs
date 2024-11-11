using System.Collections;
using ElectrumGames.MVP;
using ElectrumGames.UI.Presenters;
using UnityEngine;

namespace ElectrumGames.UI.Views
{
    [AutoRegisterView]
    public class WarningView : View<WarningPresenter>
    {
        [SerializeField] private float showTime;

        private void Start()
        {
            StartCoroutine(AwaitProcess());
        }

        private IEnumerator AwaitProcess()
        {
            yield return new WaitForSeconds(showTime);
            Presenter.ShowNextView();
        }
    }
}