using System;
using ElectrumGames.Core.Journal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Components
{
    public class EvidenceUiElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Toggle stateToggle;
        [SerializeField] private GameObject crossedOutLine;
        [SerializeField] private Button evidenceButton;

        private JournalItemState _uiJournalElementState;
        private Action<JournalItemState> _cashedEvent;
        private bool _isInited;
        
        public void Init(string evidenceName, JournalItemState uiJournalElementState, Action<JournalItemState> onButtonClick)
        {
            nameText.text = evidenceName;
            
            _uiJournalElementState = uiJournalElementState;
            _cashedEvent = onButtonClick;
            evidenceButton.onClick.AddListener(() => _cashedEvent?.Invoke(_uiJournalElementState));
            evidenceButton.onClick.AddListener(OnChangeVisual);
            
            SetState(_uiJournalElementState);
            
            _isInited = true;
        }

        private void OnDestroy()
        {
            if (!_isInited)
                return;
            
            evidenceButton.onClick.RemoveListener(() => _cashedEvent?.Invoke(_uiJournalElementState));
            evidenceButton.onClick.RemoveListener(OnChangeVisual);
        }

        private void OnChangeVisual()
        {
            if (_uiJournalElementState == JournalItemState.Unselected)
                _uiJournalElementState = JournalItemState.Selected;
            else if (_uiJournalElementState == JournalItemState.Selected)
                _uiJournalElementState = JournalItemState.Deselected;
            else
                _uiJournalElementState = JournalItemState.Unselected;
            
            SetState(_uiJournalElementState);
        }

        private void SetState(JournalItemState uiJournalElementState)
        {
            stateToggle.isOn = uiJournalElementState == JournalItemState.Selected;
            crossedOutLine.SetActive(uiJournalElementState == JournalItemState.Deselected);
        }
    }
}