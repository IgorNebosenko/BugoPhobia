using System;
using ElectrumGames.Core.Journal;
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

        private JournalItemState _journalElementState;
        private bool _isNormalState;
        
        private Action<JournalItemState> _cashedEvent;
        private bool _isInited;

        public void Init(string ghostName, JournalItemState state, bool isNormalState, 
            Action<JournalItemState> onClick)
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
            if (_journalElementState == JournalItemState.Unselected)
                _journalElementState = JournalItemState.Selected;
            else if (_journalElementState == JournalItemState.Selected)
                _journalElementState = JournalItemState.Deselected;
            else
                _journalElementState = JournalItemState.Unselected;
            
            ChangeState(_journalElementState, _isNormalState);
        }

        public void SetState(bool state)
        {
            _isNormalState = state;
            ChangeState(_journalElementState, _isNormalState);
        }

        private void ChangeState(JournalItemState state, bool isNormalState)
        {
            selectionImage.gameObject.SetActive(state == JournalItemState.Selected);
            crossedOutLine.gameObject.SetActive(state == JournalItemState.Deselected);
            
            nameText.color = isNormalState ? normalTextColor : impossibleColor;
            crossedOutLine.color = isNormalState ? normalTextColor : impossibleColor;
        }
    }
}