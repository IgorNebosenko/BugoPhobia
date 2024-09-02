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
        
        private readonly InputActions _inputActions;
        
        public SettingsPresenter(UserConfig userConfig, PlayerConfig playerConfig, ConfigService configService,
            InputActions inputActions, SettingsView view) : base(view)
        {
            UserConfig = userConfig;
            PlayerConfig = playerConfig;
            ConfigService = configService;
            
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
            View.ChangeXSensitivityText(value);
        }

        public void OnYSensitivitySliderChanged(float value)
        {
            ConfigService.YSensitivity = value;
            View.ChangeYSensitivityText(value);
        }
    }
}