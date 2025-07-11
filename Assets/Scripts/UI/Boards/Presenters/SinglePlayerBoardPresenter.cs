﻿using ElectrumGames.Core.Lobby;
using ElectrumGames.Core.Missions;
using UnityEngine;
using Zenject;

namespace ElectrumGames.UI.Boards.Presenters
{
    public class SinglePlayerBoardPresenter : MonoBehaviour
    {
        [SerializeField] private BoardsUiController boardsUiController;
        
        public MoneysHandler MoneysHandler { get; private set; }
        public LobbyItemsHandler LobbyItemsHandler { get; private set; }
        public LevelsHandler LevelsHandler { get; private set; }
        public MissionDataHandler MissionDataHandler { get; private set; }
        
        [Inject]
        private void Construct(MoneysHandler moneysHandler, LobbyItemsHandler lobbyItemsHandler, LevelsHandler levelsHandler,
            MissionDataHandler missionDataHandler)
        {
            MoneysHandler = moneysHandler;
            LobbyItemsHandler = lobbyItemsHandler;
            LevelsHandler = levelsHandler;
            MissionDataHandler = missionDataHandler;
        }

        public void OnButtonBackClicked()
        {
            boardsUiController.ShowBoardWithType(DisplayBoardsMenu.MainMenu);
        }
    }
}