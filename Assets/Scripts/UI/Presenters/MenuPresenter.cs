using ElectrumGames.Core.Player.Movement;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Views;
using UnityEngine;

namespace ElectrumGames.UI.Presenters
{
    public class MenuPresenter : Presenter<MenuView>
    {
        private readonly ViewManager _viewManager;
        private readonly InputActions _inputActions;
        
        public MenuPresenter(ViewManager viewManager, InputActions inputActions,  MenuView view) : base(view)
        {
            _viewManager = viewManager;
            _inputActions = inputActions;
        }

        public void OnContinueButtonClicked()
        {
            _viewManager.ShowView<InGamePresenter>();
        }

        public void OnSettingsButtonClicked()
        {
            _viewManager.ShowView<SettingsPresenter>();
        }

        public void OnExitButtonClicked()
        {
            Application.Quit();
        }

        protected override void Init()
        {
            Cursor.visible = true;
            _inputActions.Moving.Disable();
            _inputActions.Interactions.Disable();
        }

        protected override void Closing()
        {
            Cursor.visible = false;
            _inputActions.Moving.Enable();
            _inputActions.Interactions.Enable();
        }
    }
}