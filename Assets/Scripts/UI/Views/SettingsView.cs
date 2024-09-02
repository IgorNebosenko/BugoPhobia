using System;
using System.Globalization;
using ElectrumGames.MVP;
using ElectrumGames.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Views
{
    [AutoRegisterView]
    public class SettingsView : View<SettingsPresenter>
    {
        private enum SettingsTab
        {
            Game,
            Video,
            Audio,
            Controls
        }
        
        [SerializeField] private Button gameButton;
        [SerializeField] private Button videoButton;
        [SerializeField] private Button audioButton;
        [SerializeField] private Button controlsButton;
        [Space]
        [SerializeField] private GameObject gameTab;
        [SerializeField] private GameObject videoTab;
        [SerializeField] private GameObject audioTab;
        [SerializeField] private GameObject controlsTab;
        [Space]
        [SerializeField] private Slider xSensitivitySlider;
        [SerializeField] private TMP_Text xSensitivityText;
        [Space]
        [SerializeField] private Slider ySensitivitySlider;
        [SerializeField] private TMP_Text ySensitivityText;

        private void Start()
        {
            gameButton.onClick.AddListener(() => SwitchTab(SettingsTab.Game));
            videoButton.onClick.AddListener(() => SwitchTab(SettingsTab.Video));
            audioButton.onClick.AddListener(() => SwitchTab(SettingsTab.Audio));
            controlsButton.onClick.AddListener(() => SwitchTab(SettingsTab.Controls));

            xSensitivitySlider.minValue = Presenter.UserConfig.MinXSensitivity;
            xSensitivitySlider.maxValue = Presenter.UserConfig.MaxXSensitivity;
            xSensitivitySlider.value = Presenter.ConfigService.XSensitivity;
            xSensitivitySlider.onValueChanged.AddListener(Presenter.OnXSensitivitySliderChanged);
            
            ySensitivitySlider.minValue = Presenter.UserConfig.MinYSensitivity;
            ySensitivitySlider.maxValue = Presenter.UserConfig.MaxYSensitivity;
            ySensitivitySlider.value = Presenter.ConfigService.YSensitivity;
            ySensitivitySlider.onValueChanged.AddListener(Presenter.OnYSensitivitySliderChanged);
            
            ChangeXSensitivityText(Presenter.ConfigService.XSensitivity);
            ChangeYSensitivityText(Presenter.ConfigService.YSensitivity);
        }

        private void OnDestroy()
        {
            gameButton.onClick.RemoveListener(() => SwitchTab(SettingsTab.Game));
            videoButton.onClick.RemoveListener(() => SwitchTab(SettingsTab.Video));
            audioButton.onClick.RemoveListener(() => SwitchTab(SettingsTab.Audio));
            controlsButton.onClick.RemoveListener(() => SwitchTab(SettingsTab.Controls));
            
            xSensitivitySlider.onValueChanged.RemoveListener(Presenter.OnXSensitivitySliderChanged);
            ySensitivitySlider.onValueChanged.RemoveListener(Presenter.OnYSensitivitySliderChanged);
        }


        private void SwitchTab(SettingsTab settingsTab)
        {
            gameTab.SetActive(settingsTab == SettingsTab.Game);
            videoTab.SetActive(settingsTab == SettingsTab.Video);
            audioTab.SetActive(settingsTab == SettingsTab.Audio);
            controlsTab.SetActive(settingsTab == SettingsTab.Controls);
        }

        public void ChangeXSensitivityText(float xSensitivity)
        {
            xSensitivityText.text = Math.Round(xSensitivity, 1).ToString(CultureInfo.InvariantCulture);
        }
        
        public void ChangeYSensitivityText(float ySensitivity)
        {
            ySensitivityText.text = Math.Round(ySensitivity, 1).ToString(CultureInfo.InvariantCulture);
        }
    }
}