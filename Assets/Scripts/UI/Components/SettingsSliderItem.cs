using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Components
{
    public class SettingsSliderItem : SettingsItemBase
    {
        public enum DisplayDigitsMode
        {
            OneAfterComa,
            TwoAfterComa,
            Rounded,
            AsHundredPercents
        }
        
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text textValue;

        private bool _isInited;
        private Action<float> _cashedEvent;
        
        private DisplayDigitsMode _digitsMode;

        public void Init(string tileName, float sliderMinVal, float sliderMaxVal,
            float currentVal, Action<float> onValueChanged, DisplayDigitsMode digitsMode)
        {
            nameText.text = tileName;
            
            slider.minValue = sliderMinVal;
            slider.maxValue = sliderMaxVal;
            
            slider.value = currentVal;

            _cashedEvent = onValueChanged;
            slider.onValueChanged.AddListener(_cashedEvent.Invoke);
            
            _digitsMode = digitsMode;
            slider.onValueChanged.AddListener(OnValueChanged);
            
            OnValueChanged(currentVal);
            
            _isInited = true;
        }

        private void OnDestroy()
        {
            if (!_isInited)
                return;
            
            slider.onValueChanged.RemoveListener(_cashedEvent.Invoke);
            slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            switch (_digitsMode)
            {
                case DisplayDigitsMode.OneAfterComa:
                    textValue.text = Math.Round(value, 1).ToString(CultureInfo.InvariantCulture);
                    break;
                case DisplayDigitsMode.TwoAfterComa:
                    textValue.text = Math.Round(value, 2).ToString(CultureInfo.InvariantCulture);
                    break;
                case DisplayDigitsMode.Rounded:
                    textValue.text = Mathf.RoundToInt(value).ToString();
                    break;
                case DisplayDigitsMode.AsHundredPercents:
                    textValue.text = Mathf.RoundToInt(value * 100).ToString();
                    break;
                default:
                    textValue.text = Mathf.RoundToInt(value).ToString();
                    break;
            }
        }
    }
}