using ElectrumGames.Configs;
using ElectrumGames.Core.Player;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using Zenject;

namespace ElectrumGames.EntryPoints
{
    public class TestSceneEntry : MonoBehaviour
    {
        [SerializeField] private Transform[] playerSpawnpoints;
        
        private PlayersFactory _playersFactory;
        private ViewManager _viewManager;
        private ConfigService _configService;
        
        [Inject]
        private void Construct(PlayersFactory playersFactory, ViewManager viewManager, ConfigService configService)
        {
            _playersFactory = playersFactory;
            _viewManager = viewManager;
            _configService = configService;
        }

        private void Start()
        {
            _configService.FpsConfig = _configService.FpsConfig;
            Cursor.visible = false;
            
            Debug.LogWarning("All player creates as host and playable!");
            _playersFactory.CreatePlayer(
                true, true, playerSpawnpoints[0].position, playerSpawnpoints[0].rotation);

#if UNITY_STANDALONE
            _viewManager.ShowView<InGamePresenter>();
#elif UNITY_ANDROID
            _viewManager.ShowView<InGameAndroidPresenter>();
#endif
        }
    }
}
