using ElectrumGames.Configs;
using ElectrumGames.Core.Player.Movement;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ActivityConfig activityConfig;
    [SerializeField] private EvidenceConfig evidenceConfig;
    [Space]
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private UserConfig userConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(activityConfig).AsSingle();
        Container.BindInstance(evidenceConfig).AsSingle();
        
        Container.BindInstance(playerConfig).AsSingle();
        
        Container.Bind<ConfigService>().AsSingle().NonLazy();
        Container.BindInstance(userConfig).AsSingle();

        Container.Bind<InputActions>().AsSingle();
    }
}