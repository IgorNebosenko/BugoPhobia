using ElectrumGames.Configs;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.MVP;
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
        
        private readonly InputActions _inputActions;
        
        public SettingsPresenter(UserConfig userConfig, PlayerConfig playerConfig, ConfigService configService,
            FpsConfig fpsConfig, InputActions inputActions, SettingsView view, ScreenResolutionService screenResolution) : base(view)
        {
            UserConfig = userConfig;
            PlayerConfig = playerConfig;
            ConfigService = configService;
            FpsConfig = fpsConfig;
            ScreenResolution = screenResolution;
            
            _inputActions = inputActions;
        }
        
        protected override void Init()
        {
            Cursor.visible = true;
            _inputActions.Player.Disable();
        }

        protected override void Closing()
        {
            Cursor.visible = false;
            _inputActions.Player.Enable();
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
            ScreenResolution.SetResolutionById(value);
        }

        public void OnChangeFps(int value)
        {
            ConfigService.FpsConfig = value;
        }

        public void OnChangeFov(float value)
        {
        }

        public void OnChangeMusicVolume(float value)
        {
        }

        public void OnChangeSoundsVolume(float value)
        {
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
    }
}