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

        private EvidenceState _evidenceState;
        private Action<EvidenceState> _cashedEvent;
        private bool _isInited;
        
        public void Init(string evidenceName, EvidenceState evidenceState, Action<EvidenceState> onButtonClick)
        {
            nameText.text = evidenceName;
            
            _evidenceState = evidenceState;
            _cashedEvent = onButtonClick;
            evidenceButton.onClick.AddListener(() => _cashedEvent?.Invoke(_evidenceState));
            evidenceButton.onClick.AddListener(OnChangeVisual);
            
            SetState(_evidenceState);
            
            _isInited = true;
        }

        private void OnDestroy()
        {
            if (!_isInited)
                return;
            
            evidenceButton.onClick.RemoveListener(() => _cashedEvent?.Invoke(_evidenceState));
            evidenceButton.onClick.RemoveListener(OnChangeVisual);
        }

        private void OnChangeVisual()
        {
            if (_evidenceState == EvidenceState.Unselected)
                _evidenceState = EvidenceState.Selected;
            else if (_evidenceState == EvidenceState.Selected)
                _evidenceState = EvidenceState.Deselected;
            else
                _evidenceState = EvidenceState.Unselected;
            
            SetState(_evidenceState);
        }

        private void SetState(EvidenceState evidenceState)
        {
            switch (evidenceState)
            {
                case EvidenceState.Unselected:
                    stateToggle.isOn = false;
                    crossedOutLine.SetActive(false);
                    break;
                case EvidenceState.Selected:
                    stateToggle.isOn = true;
                    crossedOutLine.SetActive(false);
                    break;
                case EvidenceState.Deselected:
                    stateToggle.isOn = false;
                    crossedOutLine.SetActive(true);
                    break;
            }
        }
    }
}