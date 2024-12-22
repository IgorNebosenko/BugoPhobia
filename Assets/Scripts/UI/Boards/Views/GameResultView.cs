using System.Collections.Generic;
using ElectrumGames.Configs;
using ElectrumGames.Core.Lobby;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ElectrumGames.UI.Boards.Views
{
    public class GameResultView : BoardViewBase
    {
        [SerializeField] private Color activeColor;
        [SerializeField] private Color unActiveColor;
        [Space]
        [SerializeField] private string elementPattern;
        [SerializeField] private string separatorPattern;
        [SerializeField] private string ghostTypePattern;
        [Space]
        [SerializeField] private Transform infoBlock;
        [SerializeField] private TMP_Text infoBlockElement;
        [Space]
        [SerializeField] private TMP_Text ghostType;
        [SerializeField] private Button backButton;
        [Space]
        [SerializeField] private BoardsUiController boardsUiController;

        private List<TMP_Text> _infoElements;
        private MissionResultHandler _resultHandler;
        private MissionsNames _missionsNames;
        
        public override DisplayBoardsMenu DisplayBoardsMenu => DisplayBoardsMenu.HuntResult;

        [Inject]
        private void Construct(MissionResultHandler resultHandler, MissionsNames missionsNames)
        {
            _resultHandler = resultHandler;
            _missionsNames = missionsNames;
        }

        private void Start()
        {
            _infoElements = new List<TMP_Text>();
            
            Display();
        }

        private void OnEnable()
        {
            backButton.onClick.AddListener(() => boardsUiController.ShowBoardWithType(DisplayBoardsMenu.MainMenu));
        }
        
        private void OnDisable()
        {
            backButton.onClick.RemoveListener(() => boardsUiController.ShowBoardWithType(DisplayBoardsMenu.MainMenu));
        }

        public void Display()
        {
            for (var i = 0; i < _infoElements.Count; i++)
            {
                Destroy(_infoElements[i].gameObject);
            }
            _infoElements.Clear();

            var element = Instantiate(infoBlockElement, infoBlock);
            element.text = string.Format(elementPattern, "Correct ghost type", _resultHandler.CountCoinsPerGhost);
            element.color = _resultHandler.IsGhostCorrect ? activeColor : unActiveColor;
            _infoElements.Add(element);

            element = Instantiate(infoBlockElement, infoBlock);
            element.text = string.Format(elementPattern,
                _missionsNames.GetNameByType(_resultHandler.MissionsUnion.FirstMissionType),
                _resultHandler.MissionsUnion.FirstMissionStatus
                    ? MissionResultHandler.CoinsPerCorrectTask
                    : MissionResultHandler.CoinsPerIncorrectElement);
            element.color = _resultHandler.MissionsUnion.FirstMissionStatus ? activeColor : unActiveColor;
            _infoElements.Add(element);
            
            element = Instantiate(infoBlockElement, infoBlock);
            element.text = string.Format(elementPattern,
                _missionsNames.GetNameByType(_resultHandler.MissionsUnion.SecondMissionType),
                _resultHandler.MissionsUnion.SecondMissionStatus
                    ? MissionResultHandler.CoinsPerCorrectTask
                    : MissionResultHandler.CoinsPerIncorrectElement);
            element.color = _resultHandler.MissionsUnion.SecondMissionStatus ? activeColor : unActiveColor;
            _infoElements.Add(element);
            
            element = Instantiate(infoBlockElement, infoBlock);
            element.text = string.Format(elementPattern,
                _missionsNames.GetNameByType(_resultHandler.MissionsUnion.ThirdMissionType),
                _resultHandler.MissionsUnion.ThirdMissionStatus
                    ? MissionResultHandler.CoinsPerCorrectTask
                    : MissionResultHandler.CoinsPerIncorrectElement);
            element.color = _resultHandler.MissionsUnion.ThirdMissionStatus ? activeColor : unActiveColor;
            _infoElements.Add(element);
            
            element = Instantiate(infoBlockElement, infoBlock);
            element.text = separatorPattern;
            _infoElements.Add(element);
            
            element = Instantiate(infoBlockElement, infoBlock);
            element.text = string.Format(elementPattern, "Total:", _resultHandler.TotalCoins);
            _infoElements.Add(element);
            
            ghostType.text = string.Format(ghostTypePattern, _resultHandler.CorrectGhost);
        }
    }
}