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
        
        [Inject]
        private void Construct(PlayersFactory playersFactory, ViewManager viewManager)
        {
            _playersFactory = playersFactory;
            _viewManager = viewManager;
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            Cursor.visible = false;
            
            _playersFactory.CreatePlayer(
                true, playerSpawnpoints[0].position, playerSpawnpoints[0].rotation);

            _viewManager.ShowView<MenuPresenter>();
        }
    }
}
