using System;
using ElectrumGames.UI.Components.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Components
{
    public class GhostUiElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [Space]
        [SerializeField] private Color normalTextColor;
        [SerializeField] private Color impossibleColor;
        [Space]
        [SerializeField] private Image selectionImage;
        [SerializeField] private Image crossedOutLine;
        [Space]
        [SerializeField] private Button ghostButton;

        private UiJournalElementState _journalElementState;
        private bool _isNormalState;
        
        private Action<UiJournalElementState> _cashedEvent;
        private bool _isInited;

        public void Init(string ghostName, UiJournalElementState state, bool isNormalState, 
            Action<UiJournalElementState> onClick)
        {
            nameText.text = ghostName;
            
            _journalElementState = state;
            _isNormalState = isNormalState;
            
            ChangeState(_journalElementState, _isNormalState);

            _cashedEvent = onClick;
            
            ghostButton.onClick.AddListener(() => _cashedEvent?.Invoke(_journalElementState));
            ghostButton.onClick.AddListener(SwitchState);
            
            _isInited = true;
        }

        private void OnDestroy()
        {
            if (!_isInited)
                return;
            
            ghostButton.onClick.RemoveListener(() => _cashedEvent?.Invoke(_journalElementState));
            ghostButton.onClick.RemoveListener(SwitchState);
        }

        private void SwitchState()
        {
            if (_journalElementState == UiJournalElementState.Unselected)
                _journalElementState = UiJournalElementState.Selected;
            else if (_journalElementState == UiJournalElementState.Selected)
                _journalElementState = UiJournalElementState.Deselected;
            else
                _journalElementState = UiJournalElementState.Unselected;
            
            ChangeState(_journalElementState, _isNormalState);
        }

        public void SetState(bool state)
        {
            _isNormalState = state;
            ChangeState(_journalElementState, _isNormalState);
        }

        private void ChangeState(UiJournalElementState state, bool isNormalState)
        {
            selectionImage.gameObject.SetActive(state == UiJournalElementState.Selected);
            crossedOutLine.gameObject.SetActive(state == UiJournalElementState.Deselected);
            
            nameText.color = isNormalState ? normalTextColor : impossibleColor;
            crossedOutLine.color = isNormalState ? normalTextColor : impossibleColor;
        }
    }
}