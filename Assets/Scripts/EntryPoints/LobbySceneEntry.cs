using ElectrumGames.Configs;
using ElectrumGames.Core.Journal;
using ElectrumGames.Core.Player;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using Zenject;

namespace ElectrumGames.EntryPoints
{
    public class LobbySceneEntry : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;
        
        private PlayersFactory _playersFactory;
        
        private ViewManager _viewManager;
        private ConfigService _configService;
        private JournalManager _journalManager;
        
        [Inject]
        private void Construct(PlayersFactory playersFactory, ViewManager viewManager, ConfigService configService, JournalManager journalManager)
        {
            _playersFactory = playersFactory;
            
            _viewManager = viewManager;
            _configService = configService;

            _journalManager = journalManager;
        }

        private void Start()
        {
            _configService.FpsConfig = _configService.FpsConfig;

            _configService.MusicVolume = _configService.MusicVolume;
            _configService.SoundsVolume = _configService.SoundsVolume;

            Application.runInBackground = true;
            
            Debug.LogWarning("All player creates as host and playable!");
            _playersFactory.CreatePlayer(
                true, true, playerSpawnPoint.position, playerSpawnPoint.rotation);
            
            Debug.LogWarning("Read journal state before reset!");
            _journalManager.PlayerJournalInstance.Reset();
            
            for (var i = 0; i < _journalManager.OtherPlayersJournalInstances.Count; i++)
                _journalManager.OtherPlayersJournalInstances[i].Reset();
            
#if UNITY_STANDALONE
            _viewManager.ShowView<InGamePresenter>();
#elif UNITY_ANDROID
            _viewManager.ShowView<InGameAndroidPresenter>();
#endif
        }
    }
}