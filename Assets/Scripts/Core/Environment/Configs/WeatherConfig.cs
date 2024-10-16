using System;
using System.Collections.Generic;
using ElectrumGames.Core.Environment.Enums;
using UnityEngine;

namespace ElectrumGames.Core.Environment.Configs
{
    [Serializable]
    public class WeatherConfigData
    {
        [field: SerializeField] public EnvironmentWeather Weather { get; private set; }
        [field: Space]
        [field: SerializeField] public float OutdoorTemperature { get; private set; }
        [field: SerializeField] public float IndoorTemperature { get; private set; }
        [field: Space]
        [field: SerializeField] public Material SkyBoxMaterial { get; private set; }
        [field: SerializeField] public float DirectionalLightIntensity { get; private set; }
        [field: SerializeField] public Color DirectionalLightColor { get; private set; }
        [field: SerializeField] public float AmbientIntensity { get; private set; }
        [field: Space]
        [field: SerializeField] public float FogDistance { get; private set; }
        [field: SerializeField] public Color FogColor { get; private set; }
        [field: Space]
        [field: SerializeField] public AudioClip OutdoorEnvironmentMusic { get; private set; }
        [field: SerializeField] public AudioClip IndoorEnvironmentMusic { get; private set; }
        [field: Space]
        [field: SerializeField] public float NoiseOutdoorMinFrequency { get; private set; }
        [field: SerializeField] public float NoiseOutdoorMaxFrequency { get; private set; }
        [field: SerializeField] public float NoiseIndoorMinFrequency { get; private set; }
        [field: SerializeField] public float NoiseIndoorMaxFrequency { get; private set; }
        
    }

    [CreateAssetMenu(fileName = "WeatherConfig", menuName = "Environment/WeatherConfig")]
    public class WeatherConfig : ScriptableObject
    {
        [SerializeField] private WeatherConfigData[] config;

        public IReadOnlyList<WeatherConfigData> Config => config;
    }
}