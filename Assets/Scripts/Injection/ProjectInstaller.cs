using ElectrumGames.Configs;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Journal;
using ElectrumGames.Core.Lobby;
using ElectrumGames.Core.Player.Movement;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ActivityConfig activityConfig;
    [SerializeField] private EvidenceConfig evidenceConfig;
    [SerializeField] private DescriptionConfig descriptionConfig;
    [SerializeField] private LevelsConfig levelsConfig;
    [SerializeField] private DefaultMissionItems defaultMissionItems;
    [Space]
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private UserConfig userConfig;
    [Space]
    [SerializeField] private ItemsConfig itemsConfig;
    [Space]
    [SerializeField] private FpsConfig fpsConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(activityConfig).AsSingle();
        Container.BindInstance(evidenceConfig).AsSingle();
        Container.BindInstance(descriptionConfig).AsSingle();
        Container.BindInstance(levelsConfig).AsSingle();
        Container.BindInstance(defaultMissionItems).AsSingle();
        
        Container.BindInstance(playerConfig).AsSingle();
        
        Container.Bind<ConfigService>().AsSingle().NonLazy();
        Container.Bind<ScreenResolutionService>().AsSingle().NonLazy();
        
        Container.BindInstance(userConfig).AsSingle();
        
        Container.BindInstance(itemsConfig).AsSingle();
        
        Container.BindInstance(fpsConfig).AsSingle();

        Container.Bind<JournalManager>().AsSingle().NonLazy();

        Container.Bind<MoneysHandler>().AsSingle();
        Container.Bind<LobbyItemsHandler>().AsSingle();
        Container.Bind<LevelsHandler>().AsSingle();

        Container.Bind<InputActions>().AsSingle();
    }
}