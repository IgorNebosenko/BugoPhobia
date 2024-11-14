using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Views;

namespace ElectrumGames.UI.Presenters
{
    public class WarningPresenter : Presenter<WarningView>
    {
        private readonly ViewManager _viewManager;
        
        public WarningPresenter(ViewManager viewManager, WarningView view) : base(view)
        {
            _viewManager = viewManager;
        }

        public void ShowNextView()
        {
        }
    }
}