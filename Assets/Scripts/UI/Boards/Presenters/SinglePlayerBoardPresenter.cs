using ElectrumGames.Core.Lobby;
using UnityEngine;
using Zenject;

namespace ElectrumGames.UI.Boards.Presenters
{
    public class SinglePlayerBoardPresenter : MonoBehaviour
    {
        private MoneysHandler _moneysHandler;

        public decimal Moneys => _moneysHandler.Moneys;
        public LobbyItemsHandler LobbyItemsHandler { get; private set; }
        
        [Inject]
        private void Construct(MoneysHandler moneysHandler, LobbyItemsHandler lobbyItemsHandler)
        {
            _moneysHandler = moneysHandler;
            LobbyItemsHandler = lobbyItemsHandler;
        }
    }
}