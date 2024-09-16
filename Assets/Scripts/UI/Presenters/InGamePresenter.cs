using ElectrumGames.MVP;
using ElectrumGames.UI.Views;
using UnityEngine;

namespace ElectrumGames.UI.Presenters
{
    public class InGamePresenter : Presenter<InGameView>
    {
        public InGamePresenter(InGameView view) : base(view)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}