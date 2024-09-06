using System;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Components
{
    public class SettingsYesNoItem : SettingsItemBase
    {
        [Space]
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        [Space]
        [SerializeField] private Image yesImage;
        [SerializeField] private Image noImage;
        [Space]
        [SerializeField] private Color unselectedColor;
        [SerializeField] private Color selectedColor;

        private Action<bool> _cashedEvent;
        private bool _isInited;
        
        public void Init(string tileName, bool selected, Action<bool> callback)
        {
            nameText.text = tileName;
            
            _cashedEvent = callback;
            yesButton.onClick.AddListener(() => _cashedEvent?.Invoke(true));
            yesButton.onClick.AddListener(() => SetSelectedState(true));
            
            noButton.onClick.AddListener(() => _cashedEvent?.Invoke(false));
            noButton.onClick.AddListener(() => SetSelectedState(false));
            
            _isInited = true;
        }

        private void OnDestroy()
        {
            if (!_isInited)
                return;
            
            yesButton.onClick.RemoveListener(() => _cashedEvent?.Invoke(true));
            yesButton.onClick.RemoveListener(() => SetSelectedState(true));
            
            noButton.onClick.RemoveListener(() => _cashedEvent?.Invoke(false));
            noButton.onClick.RemoveListener(() => SetSelectedState(false));
        }

        private void SetSelectedState(bool selected)
        {
            yesImage.color = selected ? selectedColor : unselectedColor;
            noImage.color = !selected ? selectedColor : unselectedColor;
        }
    }
}