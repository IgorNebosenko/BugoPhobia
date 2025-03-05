using ElectrumGames.Audio.Ghosts;
using ElectrumGames.Audio.Steps;
using ElectrumGames.Configs;
using ElectrumGames.Core.Environment.Configs;
using ElectrumGames.Core.Ghost;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Journal;
using ElectrumGames.Core.Lobby;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Core.Tutorial;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer audioMixer;
    [Space]
    [SerializeField] private ActivityConfig activityConfig;
    [SerializeField] private EvidenceConfig evidenceConfig;
    [SerializeField] private DescriptionConfig descriptionConfig;
    [SerializeField] private GhostModelsList ghostModelsList;
    [SerializeField] private GhostDifficultyList ghostDifficultyList;
    [SerializeField] private EmfData emfData;
    [SerializeField] private LevelsConfig levelsConfig;
    [SerializeField] private DefaultMissionItems defaultMissionItems;
    [SerializeField] private GhostFlickConfig ghostFlickConfig;
    [Space]
    [SerializeField] private WeatherConfig weatherConfig;
    [SerializeField] private TemperatureConfig temperatureConfig;
    [Space]
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private UserConfig userConfig;
    [Space]
    [SerializeField] private ItemsConfig itemsConfig;
    [Space]
    [SerializeField] private FpsConfig pcFpsConfig;
    [SerializeField] private FpsConfig androidFpsConfig;
    [Space]
    [SerializeField] private MissionsNames missionsNames;
    [Space]
    [SerializeField] private SurfaceSoundsList surfaceSoundsList;
    [SerializeField] private SoundsConfig soundsConfig;
    [SerializeField] private GhostHuntingSounds ghostHuntingSounds;
    [SerializeField] private GhostAppearSounds ghostAppearSounds;
    [SerializeField] private RadiationConfig radiationConfig;
    [SerializeField] private SpiritBoxConfig spiritBoxConfig;
    [SerializeField] private TorchConfig torchConfig;
    [SerializeField] private GhostWritableConfig ghostWritableConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(audioMixer).AsSingle();
        
        Container.BindInstance(activityConfig).AsSingle();
        Container.BindInstance(evidenceConfig).AsSingle();
        Container.BindInstance(descriptionConfig).AsSingle();
        Container.BindInstance(ghostModelsList).AsSingle();
        Container.BindInstance(ghostDifficultyList).AsSingle();
        Container.BindInstance(emfData).AsSingle();
        Container.BindInstance(levelsConfig).AsSingle();
        Container.BindInstance(defaultMissionItems).AsSingle();
        Container.BindInstance(ghostFlickConfig).AsSingle();
        Container.BindInstance(weatherConfig).AsSingle();
        Container.BindInstance(temperatureConfig).AsSingle();
        
        Container.BindInstance(playerConfig).AsSingle();
        
        Container.Bind<ConfigService>().AsSingle().NonLazy();
        Container.Bind<ScreenResolutionService>().AsSingle().NonLazy();
        
        Container.BindInstance(userConfig).AsSingle();
        
        Container.BindInstance(itemsConfig).AsSingle();
        
#if UNITY_STANDALONE
        Container.BindInstance(pcFpsConfig).AsSingle();
#elif UNITY_ANDROID
        Container.BindInstance(androidFpsConfig).AsSingle();
#endif
        Container.BindInstance(missionsNames).AsSingle();

        Container.BindInstance(surfaceSoundsList).AsSingle();
        Container.BindInstance(soundsConfig).AsSingle();
        Container.BindInstance(ghostHuntingSounds).AsSingle();
        Container.BindInstance(ghostAppearSounds).AsSingle();
        Container.BindInstance(radiationConfig).AsSingle();
        Container.BindInstance(spiritBoxConfig).AsSingle();
        Container.BindInstance(torchConfig).AsSingle();
        Container.BindInstance(ghostWritableConfig).AsSingle();

        Container.Bind<TutorialHandler>().AsSingle();

        Container.Bind<JournalManager>().AsSingle().NonLazy();

        Container.Bind<MoneysHandler>().AsSingle();
        Container.Bind<LobbyItemsHandler>().AsSingle();
        Container.Bind<LevelsHandler>().AsSingle();

        Container.Bind<MissionDataHandler>().AsSingle();

        Container.Bind<InputActions>().AsSingle();
        
        Container.Bind<GhostEnvironmentHandler>().AsSingle();
    }
}