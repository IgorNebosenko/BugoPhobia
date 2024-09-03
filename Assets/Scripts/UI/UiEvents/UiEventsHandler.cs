using System;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.MVP.Managers;
using UniRx;
using UnityEngine;

namespace ElectrumGames.UI.UiEvents
{
    public class UiEventsHandler
    {
        private readonly ViewManager _viewManager;
        private readonly UiEventInput _eventInput;

        private IDisposable _updateProcess;
        
        private bool _journalLastState;
        private bool _menuLastState;
        
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
                    Debug.Log("journal pressed");
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
                    Debug.Log("menu pressed");
                }
            }
            else
            {
                _menuLastState = false;
            }
        }
    }
}