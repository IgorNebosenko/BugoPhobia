using System;
using ElectrumGames.Core.Journal;
using ElectrumGames.GlobalEnums;
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
        
        public GhostType GhostType { get; private set; }

        public void Init(string ghostName, JournalItemState state, bool isNormalState, 
            Action<JournalItemState> onClick, GhostType ghostType)
        {
            nameText.text = ghostName;
            
            _journalElementState = state;
            _isNormalState = isNormalState;
            
            ChangeState(_journalElementState, _isNormalState);

            _cashedEvent = onClick;

            GhostType = ghostType;
            
            ghostButton.onClick.AddListener(() => _cashedEvent?.Invoke(_journalElementState));
            
            _isInited = true;
        }

        private void OnDestroy()
        {
            if (!_isInited)
                return;
            
            ghostButton.onClick.RemoveListener(() => _cashedEvent?.Invoke(_journalElementState));
        }

        public void SetState(bool state)
        {
            _isNormalState = state;
            ChangeState(_journalElementState, _isNormalState);
        }

        public void UpdateSelection(JournalItemState state)
        {
            ChangeState(state, _isNormalState);
        }

        private void ChangeState(JournalItemState state, bool isNormalState)
        {
            _journalElementState = state;
            
            selectionImage.gameObject.SetActive(_journalElementState == JournalItemState.Selected);
            crossedOutLine.gameObject.SetActive(_journalElementState == JournalItemState.Deselected);
            
            nameText.color = isNormalState ? normalTextColor : impossibleColor;
            crossedOutLine.color = isNormalState ? normalTextColor : impossibleColor;
        }
    }
}