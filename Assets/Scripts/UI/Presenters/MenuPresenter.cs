﻿using ElectrumGames.Core.Player.Movement;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Views;
using UnityEngine;

namespace ElectrumGames.UI.Presenters
{
    public class MenuPresenter : Presenter<MenuView>
    {
        private readonly ViewManager _viewManager;
        private readonly PopupManager _popupManager;
        private readonly InputActions _inputActions;
        
        public MenuPresenter(ViewManager viewManager, PopupManager popupManager, InputActions inputActions,
            MenuView view) : base(view)
        {
            _viewManager = viewManager;
            _popupManager = popupManager;
            
            _inputActions = inputActions;
        }

        public void OnContinueButtonClicked()
        {
#if UNITY_STANDALONE
            _viewManager.ShowView<InGamePresenter>();
#elif UNITY_ANDROID
            _viewManager.ShowView<InGameAndroidPresenter>();
#endif
        }
        
        public void OnDebugButtonClicked()
        {
            _popupManager.ShowPopup<DebugPopupPresenter>();
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _inputActions.Moving.Disable();
            _inputActions.Interactions.Disable();
        }

        protected override void Closing()
        {
#if UNITY_STANDALONE
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
#endif
            _inputActions.Moving.Enable();
            _inputActions.Interactions.Enable();
        }
    }
}