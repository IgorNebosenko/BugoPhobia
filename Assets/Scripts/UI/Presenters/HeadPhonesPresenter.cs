using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Views;

namespace ElectrumGames.UI.Presenters
{
    public class HeadPhonesPresenter : Presenter<HeadPhonesView>
    {
        private readonly ViewManager _viewManager;
        
        public HeadPhonesPresenter(ViewManager viewManager, HeadPhonesView view) : base(view)
        {
            _viewManager = viewManager;
        }
        
        public void ShowNextView()
        {
            //_viewManager.ShowView<HeadPhonesPresenter>();
        }
    }
}