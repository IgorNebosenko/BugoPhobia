using UnityEngine;

namespace ElectrumGames.UI.Boards.Presenters
{
    public class SpecialThanksBoardPresenter : MonoBehaviour
    {
        [SerializeField] private BoardsUiController boardsUiController;

        public void OnBackButtonClicked()
        {
            boardsUiController.ShowBoardWithType(DisplayBoardsMenu.MainMenu);
        }
    }
}