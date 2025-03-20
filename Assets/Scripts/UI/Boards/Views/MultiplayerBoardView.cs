using ElectrumGames.UI.Boards.Presenters;
using UnityEngine;

namespace ElectrumGames.UI.Boards.Views
{
    public class MultiplayerBoardView : BoardViewBase
    {
        [SerializeField] private MultiplayerBoardPresenter presenter;
        
        public override DisplayBoardsMenu DisplayBoardsMenu => DisplayBoardsMenu.MultiPlayer;
    }
}