using ElectrumGames.UI.Boards.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Boards.Views
{
    public class SpecialThanksBoardView : BoardViewBase
    {
        [SerializeField] private Button backButton;
        [Space]
        [SerializeField] private SpecialThanksBoardPresenter presenter;
        
        public override DisplayBoardsMenu DisplayBoardsMenu => DisplayBoardsMenu.SpecialThanks;

        private void Start()
        {
            backButton.onClick.AddListener(presenter.OnBackButtonClicked);
        }

        private void OnDestroy()
        {
            backButton.onClick.RemoveListener(presenter.OnBackButtonClicked);
        }
    }
}