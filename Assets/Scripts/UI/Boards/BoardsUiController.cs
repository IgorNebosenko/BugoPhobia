using ElectrumGames.Core.Lobby;
using ElectrumGames.GlobalEnums;
using ElectrumGames.UI.Boards.Views;
using UnityEngine;
using Zenject;

namespace ElectrumGames.UI.Boards
{
    public class BoardsUiController : MonoBehaviour
    {
        [SerializeField] private BoardViewBase[] views;

        private MissionResultHandler _resultHandler;

        [Inject]
        private void Construct(MissionResultHandler resultHandler)
        {
            _resultHandler = resultHandler;
        }
        
        private void Start()
        {
            ShowBoardWithType(_resultHandler.CorrectGhost == GhostType.None
                ? DisplayBoardsMenu.MainMenu
                : DisplayBoardsMenu.HuntResult);
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
