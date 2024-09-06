using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ElectrumGames.Configs;
using ElectrumGames.MVP;
using ElectrumGames.UI.Components;
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

        [SerializeField, FoldoutGroup("Items templates")] private SettingsSliderItem settingsSliderItem;
        [SerializeField, FoldoutGroup("Items templates")] private SettingsSelectableItem settingsSelectableItem;
        [SerializeField, FoldoutGroup("Items templates")] private SettingsButtonItem settingsButtonItem;
        [SerializeField, FoldoutGroup("Items templates")] private SettingsYesNoItem settingsYesNoItem;
        
        [SerializeField, FoldoutGroup("Tabs")] private GameObject gameTab;
        [SerializeField, FoldoutGroup("Tabs")] private GameObject videoTab;
        [SerializeField, FoldoutGroup("Tabs")] private GameObject audioTab;
        [SerializeField, FoldoutGroup("Tabs")] private GameObject controlsTab;
        [Space]
        [SerializeField, FoldoutGroup("Tabs")] private Transform gameTabContainer;
        [SerializeField, FoldoutGroup("Tabs")] private Transform videoTabContainer;
        [SerializeField, FoldoutGroup("Tabs")] private Transform audioTabContainer;
        [SerializeField, FoldoutGroup("Tabs")] private Transform controlsTabContainer;

        private void Start()
        {
            #region Game
            
            Instantiate(settingsSliderItem, gameTabContainer).Init("X Sensitivity", 
                Presenter.UserConfig.MinXSensitivity, Presenter.UserConfig.MaxXSensitivity, 
                Presenter.ConfigService.XSensitivity, Presenter.OnXSensitivitySliderChanged, 
                SettingsSliderItem.DisplayDigitsMode.OneAfterComa);
            
            Instantiate(settingsSliderItem, gameTabContainer).Init("Y Sensitivity", 
                Presenter.UserConfig.MinYSensitivity, Presenter.UserConfig.MaxYSensitivity, 
                Presenter.ConfigService.YSensitivity, Presenter.OnYSensitivitySliderChanged, 
                SettingsSliderItem.DisplayDigitsMode.OneAfterComa);
            
            Instantiate(settingsYesNoItem, gameTabContainer).Init("Invert X",
                Presenter.ConfigService.EnableXInversion, x => Presenter.ConfigService.EnableXInversion = x);
            
            Instantiate(settingsYesNoItem, gameTabContainer).Init("Invert Y",
                Presenter.ConfigService.EnableYInversion, x => Presenter.ConfigService.EnableYInversion = x);
            
            Instantiate(settingsYesNoItem, gameTabContainer).Init("Headbob",
                Presenter.ConfigService.EnableHeadBob, x => Presenter.ConfigService.EnableHeadBob = x);
            
            Instantiate(settingsSelectableItem, gameTabContainer).Init("*Language", 
                new List<string>{"English"}, Presenter.OnChangeLanguage, 0);
            
            Instantiate(settingsSelectableItem, gameTabContainer).Init("*Voice",
                new List<string>{"English"}, Presenter.OnChangeVoice, 0);
            
            Instantiate(settingsSelectableItem, gameTabContainer).Init("*Recognition system",
                new List<string>{"System", "Vosk", "Text", "None"}, Presenter.OnChangeRecognitionSystem,
                0);
            
            #endregion

            #region Video
            
            // Instantiate(settingsSelectableItem, videoTabContainer).Init("*Resolution",
            //     resolutions, Presenter.OnChangeResolution, 0);
            
            Instantiate(settingsYesNoItem, videoTabContainer).Init("Fullscreen",
                Presenter.ConfigService.IsFullScreen,
                x => Presenter.ConfigService.IsFullScreen = x);
            
            Instantiate(settingsSelectableItem, videoTabContainer).Init("FPS",
                Presenter.FpsConfig.Data.Select(x => x.fpsName).ToList(), 
                Presenter.OnChangeFps, Presenter.ConfigService.FpsConfig);
            
            Instantiate(settingsSliderItem, videoTabContainer).Init("*FOV", 
                Presenter.UserConfig.MinFOV, Presenter.UserConfig.MaxFOV, 
                Presenter.ConfigService.FOV, Presenter.OnChangeFov, 
                SettingsSliderItem.DisplayDigitsMode.Rounded);
            
            #endregion

            #region Audio
            
            Instantiate(settingsSliderItem, audioTabContainer).Init("*Music", 
                0f, 1f, 
                1f, Presenter.OnChangeMusicVolume, 
                SettingsSliderItem.DisplayDigitsMode.AsHundredPercents);
            Instantiate(settingsSliderItem, audioTabContainer).Init("*Sounds", 
                0f, 1f, 
                1f, Presenter.OnChangeSoundsVolume, 
                SettingsSliderItem.DisplayDigitsMode.AsHundredPercents);
            
            Instantiate(settingsSelectableItem, audioTabContainer).Init("*Output device",
                new List<string>{"Default"}, Presenter.OnOutputDeviceChanged, 0);
            
            Instantiate(settingsSelectableItem, audioTabContainer).Init("*Input device",
                new List<string>{"Default"}, Presenter.OnInputDeviceChanged, 0);

            #endregion

            #region Controls
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Forward", "W", 
                Presenter.OnForwardActionChanged);
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Backward", "S", 
                Presenter.OnBackwardActionChanged);
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Left", "A", 
                Presenter.OnLeftActionChanged);
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Right", "D", 
                Presenter.OnRightActionChanged);
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Sprint", "Shift", 
                Presenter.OnSprintActionChanged);
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Crouch", "C", 
                Presenter.OnCrouchActionChanged);
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Primary interaction", "LMB", 
                Presenter.OnPrimaryActionChanged);
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Alternative interaction", "RBM", 
                Presenter.OnAlternativeActionChanged);
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Voice", "T", 
                Presenter.OnVoiceActionChanged);
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Journal", "J", 
                Presenter.OnJournalActionChanged);
            
            Instantiate(settingsButtonItem, controlsTabContainer).Init("*Menu", "Escape", 
                Presenter.OnMenuActionChanged);
            
            #endregion
            
            SwitchTab(SettingsTab.Game);
            
            gameButton.onClick.AddListener(() => SwitchTab(SettingsTab.Game));
            videoButton.onClick.AddListener(() => SwitchTab(SettingsTab.Video));
            audioButton.onClick.AddListener(() => SwitchTab(SettingsTab.Audio));
            controlsButton.onClick.AddListener(() => SwitchTab(SettingsTab.Controls));
            
            //TODO Change language
            //TODO Change voice
            
            //TODO resolution
            //TODO FPS
            //TODO FOV
            
            //TODO music
            //TODO sounds
            //TODO input device
            //TODO output device
        }

        private void OnDestroy()
        {
            gameButton.onClick.RemoveListener(() => SwitchTab(SettingsTab.Game));
            videoButton.onClick.RemoveListener(() => SwitchTab(SettingsTab.Video));
            audioButton.onClick.RemoveListener(() => SwitchTab(SettingsTab.Audio));
            controlsButton.onClick.RemoveListener(() => SwitchTab(SettingsTab.Controls));
        }


        private void SwitchTab(SettingsTab settingsTab)
        {
            gameTab.SetActive(settingsTab == SettingsTab.Game);
            videoTab.SetActive(settingsTab == SettingsTab.Video);
            audioTab.SetActive(settingsTab == SettingsTab.Audio);
            controlsTab.SetActive(settingsTab == SettingsTab.Controls);
        }
    }
}