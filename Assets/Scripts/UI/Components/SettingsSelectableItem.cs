using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Components
{
    public class SettingsSelectableItem : SettingsItemBase
    {
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;

        [SerializeField] private TMP_Text itemText;

        private List<string> _variants;

        private Action<int> _cashedEvent;
        private int _selectedVariant;

        private bool _isInited;
        
        public void Init(string tileName, List<string> variants, Action<int> onVariantSelected, int selectedVariant)
        {
            nameText.text = tileName;
            
            _variants = variants;

            _cashedEvent = onVariantSelected;
            
            previousButton.onClick.AddListener(OnPreviousButtonClicked);
            nextButton.onClick.AddListener(OnNextButtonClicked);
            
            _selectedVariant = selectedVariant;
            itemText.text = _variants[_selectedVariant];
            
            _isInited = true;
        }

        private void OnDestroy()
        {
            if (!_isInited)
                return;
            
            previousButton.onClick.RemoveListener(OnPreviousButtonClicked);
            nextButton.onClick.RemoveListener(OnNextButtonClicked);
        }

        private void OnPreviousButtonClicked()
        {
            if (--_selectedVariant < 0)
                _selectedVariant = _variants.Count - 1;
            
            _cashedEvent?.Invoke(_selectedVariant);
            itemText.text = _variants[_selectedVariant];
        }

        private void OnNextButtonClicked()
        {
            if (++_selectedVariant >= _variants.Count)
                _selectedVariant = 0;
            
            _cashedEvent?.Invoke(_selectedVariant);
            itemText.text = _variants[_selectedVariant];
        }
    }
}