using ElectrumGames.Core.Lobby;
using UnityEngine;
using Zenject;

namespace ElectrumGames.UI.Boards.Presenters
{
    public class SinglePlayerBoardPresenter : MonoBehaviour
    {
        private MoneysHandler _moneysHandler;


        public decimal Moneys => _moneysHandler.Moneys;
        
        [Inject]
        private void Construct(MoneysHandler moneysHandler)
        {
            _moneysHandler = moneysHandler;
        }
    }
}