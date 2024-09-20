using ElectrumGames.Core.Lobby;
using UnityEngine;
using Zenject;

namespace ElectrumGames.UI.Boards.Presenters
{
    public class SinglePlayerBoardPresenter : MonoBehaviour
    {
        public MoneysHandler MoneysHandler { get; private set; }
        public LobbyItemsHandler LobbyItemsHandler { get; private set; }
        
        [Inject]
        private void Construct(MoneysHandler moneysHandler, LobbyItemsHandler lobbyItemsHandler)
        {
            MoneysHandler = moneysHandler;
            LobbyItemsHandler = lobbyItemsHandler;
        }
    }
}