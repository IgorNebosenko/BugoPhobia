using System;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Extensions;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UniRx;

namespace ElectrumGames.UI.UiEvents
{
    public class UiEventsHandler
    {
        private readonly ViewManager _viewManager;
        private readonly UiEventInput _eventInput;

        private IDisposable _updateProcess;
        
        private bool _journalLastState;
        private bool _menuLastState;

        private JournalPresenter _journalPresenter;
        private MenuPresenter _menuPresenter;
        
        public UiEventsHandler(ViewManager viewManager, InputActions inputActions)
        {
            _viewManager = viewManager;
            _eventInput = new UiEventInput(inputActions);

            _updateProcess = Observable.EveryFixedUpdate().Subscribe(FixedUpdate);
        }

        ~UiEventsHandler()
        {
            _eventInput.OnDestroy();
        }

        private void FixedUpdate(long _)
        {
            if (_eventInput.IsJournalPressed)
            {
                if (!_journalLastState)
                {
                    _journalLastState = true;
                    if (_journalPresenter.UnityNullCheck())
                    {
                        _journalPresenter = _viewManager.ShowView<JournalPresenter>();
                        _menuPresenter = null;
                    }
                    else
                    {
#if UNITY_STANDALONE
                        _viewManager.ShowView<InGamePresenter>();
#elif UNITY_ANDROID
            _viewManager.ShowView<InGameAndroidPresenter>();
#endif
                        _journalPresenter = null;
                        _menuPresenter = null;
                    }
                }
            }
            else
            {
                _journalLastState = false;
            }

            if (_eventInput.IsMenuPressed)
            {
                if (!_menuLastState)
                {
                    _menuLastState = true;
                    if (_menuPresenter.UnityNullCheck())
                    {
                        _menuPresenter = _viewManager.ShowView<MenuPresenter>();
                        _journalPresenter = null;
                    }
                    else
                    {
#if UNITY_STANDALONE
                        _viewManager.ShowView<InGamePresenter>();
#elif UNITY_ANDROID
                        _viewManager.ShowView<InGameAndroidPresenter>();
#endif
                        _menuPresenter = null;
                        _journalPresenter = null;
                    }
                }
            }
            else
            {
                _menuLastState = false;
            }
        }
    }
}