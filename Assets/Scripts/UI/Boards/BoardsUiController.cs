using ElectrumGames.UI.Boards.Views;
using UnityEngine;

namespace ElectrumGames.UI.Boards
{
    public class BoardsUiController : MonoBehaviour
    {
        [SerializeField] private BoardViewBase[] views;

        private void Start()
        {
            ShowBoardWithType(DisplayBoardsMenu.MainMenu);
        }

        public void ShowBoardWithType(DisplayBoardsMenu type)
        {
            for (var i = 0; i < views.Length; i++)
            {
                views[i].gameObject.SetActive(views[i].DisplayBoardsMenu == type);
            }
        }
    }
}
