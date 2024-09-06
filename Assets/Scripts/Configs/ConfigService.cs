using UnityEngine;

namespace ElectrumGames.Configs
{
    public class ConfigService
    {
        private const string XSensitivityKey = "XSensitivity";
        private const string YSensitivityKey = "YSensitivity";
        
        private const string InvertXKey = "InvertXAxis";
        private const string InvertYKey = "InvertYAxis";
        
        private const string HeadBobKey = "HeadBob";
        
        private const string FOVKey = "FOV";

        private const string FpsKey = "FPS";

        private const string ResolutionXKey = "ResolutionX";
        private const string ResolutionYKey = "ResolutionY";
        private const string IsFullScreenKey = "IsFullScreen";
        
        private readonly UserConfig _userConfig;
        private readonly FpsConfig _fpsConfig;

        public float XSensitivity
        {
            get => PlayerPrefs.GetFloat(XSensitivityKey, _userConfig.DefaultXSensitivity);
            set => PlayerPrefs.SetFloat(XSensitivityKey, value);
        }

        public float YSensitivity
        {
            get => PlayerPrefs.GetFloat(YSensitivityKey, _userConfig.DefaultYSensitivity);
            set => PlayerPrefs.SetFloat(YSensitivityKey, value);
        }

        public bool EnableXInversion
        {
            get => PlayerPrefs.GetInt(InvertXKey, _userConfig.DefaultInvertXValue) != 0;
            set => PlayerPrefs.SetInt(InvertXKey, value ? 1 : 0);
        }
        
        public bool EnableYInversion
        {
            get => PlayerPrefs.GetInt(InvertYKey, _userConfig.DefaultInvertYValue) != 0;
            set => PlayerPrefs.SetInt(InvertYKey, value ? 1 : 0);
        }

        public bool EnableHeadBob
        {
            get => PlayerPrefs.GetInt(HeadBobKey, _userConfig.DefaultHeadBobValue) != 0;
            set => PlayerPrefs.SetInt(HeadBobKey, value ? 1 : 0);
        }

        public float FOV
        {
            get => PlayerPrefs.GetFloat(FOVKey, _userConfig.DefaultFOV);
            set => PlayerPrefs.SetFloat(FOVKey, value);
        }

        public int FpsConfig
        {
            get => PlayerPrefs.GetInt(FpsKey, _fpsConfig.DefaultIndex);
            set
            {
                PlayerPrefs.SetInt(FpsKey, value);
                Application.targetFrameRate = _fpsConfig.Data[value].fpsValue;
            }
        }

        public Vector2Int Resolution
        {
            get => new (PlayerPrefs.GetInt(ResolutionXKey, 1280), PlayerPrefs.GetInt(ResolutionYKey, 720));
            set
            {
                PlayerPrefs.SetInt(ResolutionXKey, value.x);
                PlayerPrefs.SetInt(ResolutionYKey, value.y);
                Screen.SetResolution(value.x, value.y, IsFullScreen);
            }
        }
        
        public bool IsFullScreen
        {
            get => PlayerPrefs.GetInt(IsFullScreenKey, 1) != 0;
            set
            {
                Screen.fullScreen = value;
                PlayerPrefs.SetInt(IsFullScreenKey, value ? 1 : 0);
            }
        }

        public ConfigService(UserConfig userConfig, FpsConfig fpsConfig)
        {
            _userConfig = userConfig;
            _fpsConfig = fpsConfig;
            
            CheckUserConfigs();
        }

        private void CheckUserConfigs()
        {
            if (!PlayerPrefs.HasKey(XSensitivityKey) || XSensitivity < _userConfig.MinXSensitivity ||
                XSensitivity > _userConfig.MaxXSensitivity)
            {
                XSensitivity = _userConfig.DefaultXSensitivity;
            }

            if (!PlayerPrefs.HasKey(YSensitivityKey) || YSensitivity < _userConfig.MinYSensitivity ||
                YSensitivity > _userConfig.MaxYSensitivity)
            {
                YSensitivity = _userConfig.DefaultYSensitivity;
            }

            if (!PlayerPrefs.HasKey(InvertXKey) || PlayerPrefs.GetInt(InvertXKey) < _userConfig.MinInvertXValue ||
                PlayerPrefs.GetInt(InvertXKey) > _userConfig.MaxInvertXValue)
            {
                PlayerPrefs.SetInt(InvertXKey, _userConfig.DefaultInvertXValue);
            }
            
            if (!PlayerPrefs.HasKey(InvertYKey) || PlayerPrefs.GetInt(InvertYKey) < _userConfig.MinInvertYValue ||
                PlayerPrefs.GetInt(InvertYKey) > _userConfig.MaxInvertYValue)
            {
                PlayerPrefs.SetInt(InvertYKey, _userConfig.DefaultInvertYValue);
            }

            if (!PlayerPrefs.HasKey(HeadBobKey) || PlayerPrefs.GetInt(HeadBobKey) < _userConfig.MinHeadBobValue ||
                PlayerPrefs.GetInt(HeadBobKey) > _userConfig.MaxHeadBobValue)
            {
                PlayerPrefs.SetInt(HeadBobKey, _userConfig.DefaultHeadBobValue);
            }

            if (!PlayerPrefs.HasKey(FOVKey) || FOV < _userConfig.MinFOV || _userConfig.MaxFOV > FOV)
            {
                FOV = _userConfig.DefaultFOV;
            }

            if (!PlayerPrefs.HasKey(FpsKey) || FpsConfig < 0 || FpsConfig >= _fpsConfig.Data.Length)
            {
                FpsConfig = _fpsConfig.DefaultIndex;
            }
        }
    }
}