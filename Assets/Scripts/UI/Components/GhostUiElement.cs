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
        private bool _isInited;

        public void Init(string ghostName, UiJournalElementState state, bool isNormalState, 
            Action<UiJournalElementState> onClick)
        {
            nameText.text = ghostName;
            
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