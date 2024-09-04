using System;
using ElectrumGames.UI.Components.Enums;
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

        private UiJournalElementState _uiJournalElementState;
        private Action<UiJournalElementState> _cashedEvent;
        private bool _isInited;
        
        public void Init(string evidenceName, UiJournalElementState uiJournalElementState, Action<UiJournalElementState> onButtonClick)
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
            if (_uiJournalElementState == UiJournalElementState.Unselected)
                _uiJournalElementState = UiJournalElementState.Selected;
            else if (_uiJournalElementState == UiJournalElementState.Selected)
                _uiJournalElementState = UiJournalElementState.Deselected;
            else
                _uiJournalElementState = UiJournalElementState.Unselected;
            
            SetState(_uiJournalElementState);
        }

        private void SetState(UiJournalElementState uiJournalElementState)
        {
            stateToggle.isOn = uiJournalElementState == UiJournalElementState.Selected;
            crossedOutLine.SetActive(uiJournalElementState == UiJournalElementState.Deselected);
        }
    }
}