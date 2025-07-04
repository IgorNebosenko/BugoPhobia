﻿using ElectrumGames.Configs;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Views;
using UnityEngine;

namespace ElectrumGames.UI.Presenters
{
    public class SettingsPresenter : Presenter<SettingsView>
    {
        public UserConfig UserConfig { get; }
        public PlayerConfig PlayerConfig { get; }
        public ConfigService ConfigService { get; }
        public FpsConfig FpsConfig { get; }
        public ScreenResolutionService ScreenResolution { get; }

        public int QualityLevel => QualitySettings.GetQualityLevel();

        private readonly Camera _injectedCamera;
        
        private readonly InputActions _inputActions;

        private readonly ViewManager _viewManager;
        
        public SettingsPresenter(UserConfig userConfig, PlayerConfig playerConfig, ConfigService configService,
            FpsConfig fpsConfig, InputActions inputActions, SettingsView view, ScreenResolutionService screenResolution,
            Camera injectedCamera, ViewManager viewManager) : base(view)
        {
            UserConfig = userConfig;
            PlayerConfig = playerConfig;
            ConfigService = configService;
            FpsConfig = fpsConfig;
            ScreenResolution = screenResolution;

            _injectedCamera = injectedCamera;
            
            _inputActions = inputActions;

            _viewManager = viewManager;
        }
        
        protected override void Init()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _inputActions.Moving.Disable();
            _inputActions.Interactions.Disable();
        }

        protected override void Closing()
        {
#if UNITY_STANDALONE
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
#endif
            _inputActions.Moving.Enable();
            
            _inputActions.UiEvents.Enable();
            _inputActions.UI.Enable();
            
            _inputActions.Interactions.Enable();
        }

        public void OnChangeQualityLevel(int value)
        {
            QualitySettings.SetQualityLevel(value);
        }

        public void OnXSensitivitySliderChanged(float value)
        {
            ConfigService.XSensitivity = value;
        }

        public void OnYSensitivitySliderChanged(float value)
        {
            ConfigService.YSensitivity = value;
        }

        public void OnChangeLanguage(int value)
        {
        }

        public void OnChangeVoice(int value)
        {
        }

        public void OnChangeRecognitionSystem(int value)
        {
        }

        public void OnChangeResolution(int value)
        {
            ConfigService.Resolution = value;
            ScreenResolution.SetResolutionById(value);
        }

        public void OnChangeFps(int value)
        {
            ConfigService.FpsConfig = value;
        }

        public void OnChangeFov(float value)
        {
            ConfigService.FOV = value;
            _injectedCamera.fieldOfView = value;
        }

        public void OnChangeMusicVolume(float value)
        {
            ConfigService.MusicVolume = value;
        }

        public void OnChangeSoundsVolume(float value)
        {
            ConfigService.SoundsVolume = value;
        }

        public void OnOutputDeviceChanged(int value)
        {
        }

        public void OnInputDeviceChanged(int value)
        {
        }

        public void OnForwardActionChanged()
        {
        }
        
        public void OnBackwardActionChanged()
        {
        }
        
        public void OnLeftActionChanged()
        {
        }
        
        public void OnRightActionChanged()
        {
        }

        public void OnSprintActionChanged()
        {
        }

        public void OnCrouchActionChanged()
        {
        }

        public void OnPrimaryActionChanged()
        {
        }

        public void OnAlternativeActionChanged()
        {
        }

        public void OnVoiceActionChanged()
        {
        }

        public void OnJournalActionChanged()
        {
        }

        public void OnMenuActionChanged()
        {
        }

        public void OnCloseButtonClicked()
        {
            Closing();
            
#if UNITY_STANDALONE
            _viewManager.ShowView<InGamePresenter>();
#elif UNITY_ANDROID
            _viewManager.ShowView<InGameAndroidPresenter>();
#endif
        }
    }
}