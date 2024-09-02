using System;
using System.Globalization;
using ElectrumGames.MVP;
using ElectrumGames.UI.Presenters;
using Sirenix.OdinInspector;
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
        
        [SerializeField, FoldoutGroup("Head")] private Button gameButton;
        [SerializeField, FoldoutGroup("Head")] private Button videoButton;
        [SerializeField, FoldoutGroup("Head")] private Button audioButton;
        [SerializeField, FoldoutGroup("Head")] private Button controlsButton;

        [SerializeField, FoldoutGroup("Tabs")] private GameObject gameTab;
        [SerializeField, FoldoutGroup("Tabs")] private GameObject videoTab;
        [SerializeField, FoldoutGroup("Tabs")] private GameObject audioTab;
        [SerializeField, FoldoutGroup("Tabs")] private GameObject controlsTab;
        
        [SerializeField, FoldoutGroup("Game")] private Slider xSensitivitySlider;
        [SerializeField, FoldoutGroup("Game")] private TMP_Text xSensitivityText;
        [Space]
        [SerializeField, FoldoutGroup("Game")] private Slider ySensitivitySlider;
        [SerializeField, FoldoutGroup("Game")] private TMP_Text ySensitivityText;
        [Space]
        [SerializeField, FoldoutGroup("Game")] private Button previousLanguageButton;
        [SerializeField, FoldoutGroup("Game")] private TMP_Text languageText;
        [SerializeField, FoldoutGroup("Game")] private Button nextLanguageButton;
        [Space]
        [SerializeField, FoldoutGroup("Game")] private Button previousVoiceButton;
        [SerializeField, FoldoutGroup("Game")] private TMP_Text languageVoice;
        [SerializeField, FoldoutGroup("Game")] private Button nextVoiceButton;

        [SerializeField, FoldoutGroup("Video")] private Button previousResolutionButton;
        [SerializeField, FoldoutGroup("Video")] private TMP_Text resolutionText;
        [SerializeField, FoldoutGroup("Video")] private Button nextResolutionButton;
        [Space]
        [SerializeField, FoldoutGroup("Video")] private Button previousFpsButton;
        [SerializeField, FoldoutGroup("Video")] private TMP_Text fpsText;
        [SerializeField, FoldoutGroup("Video")] private Button nextFpsButton;
        [Space]
        [SerializeField, FoldoutGroup("Video")] private Slider fovSlider;
        [SerializeField, FoldoutGroup("Video")] private TMP_Text fovText;
        
        [SerializeField, FoldoutGroup("Sounds")] private Slider musicSlider;
        [SerializeField, FoldoutGroup("Sounds")] private TMP_Text musicText;
        
        //TODO Sounds, input, output device

        private void Start()
        {
            SwitchTab(SettingsTab.Game);
            
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
            
            //TODO Change language
            //TODO Change voice
            
            //TODO resolution
            //TODO FPS
            //TODO FOV
            
            //TODO music
            
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