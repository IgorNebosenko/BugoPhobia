using System.Collections.Generic;
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

        private List<TMP_Text> _infoElements;
        private MissionResultHandler _resultHandler;
        
        public override DisplayBoardsMenu DisplayBoardsMenu => DisplayBoardsMenu.HuntResult;

        [Inject]
        private void Construct(MissionResultHandler resultHandler)
        {
            _resultHandler = resultHandler;
        }

        private void Start()
        {
            _infoElements = new List<TMP_Text>();
            
            Display();
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
            
            ghostType.text = string.Format(ghostTypePattern, _resultHandler.CorrectGhost);
        }
    }
}