using System.Collections.Generic;
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
        
        public override DisplayBoardsMenu DisplayBoardsMenu => DisplayBoardsMenu.HuntResult;

        [Inject]
        private void Construct()
        {
        }

        private void Start()
        {
            _infoElements = new List<TMP_Text>();
        }
    }
}