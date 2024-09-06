using ElectrumGames.Configs;
using ElectrumGames.Core.Journal;
using ElectrumGames.Core.Player.Movement;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ActivityConfig activityConfig;
    [SerializeField] private EvidenceConfig evidenceConfig;
    [SerializeField] private DescriptionConfig descriptionConfig;
    [Space]
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private UserConfig userConfig;
    [Space]
    [SerializeField] private FpsConfig fpsConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(activityConfig).AsSingle();
        Container.BindInstance(evidenceConfig).AsSingle();
        Container.BindInstance(descriptionConfig).AsSingle();
        
        Container.BindInstance(playerConfig).AsSingle();
        
        Container.Bind<ConfigService>().AsSingle().NonLazy();
        Container.Bind<ScreenResolutionService>().AsSingle().NonLazy();
        
        Container.BindInstance(userConfig).AsSingle();

        Container.BindInstance(fpsConfig).AsSingle();

        Container.Bind<JournalManager>().AsSingle().NonLazy();

        Container.Bind<InputActions>().AsSingle();
    }
}