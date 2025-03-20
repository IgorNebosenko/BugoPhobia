using ElectrumGames.UI.Boards.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Boards.Views
{
    public class DemoSelectorBoardView : BoardViewBase
    {
        [SerializeField] private Button firstHouseButton;
        [SerializeField] private Button apartmentButton;
        [SerializeField] private Button smallHouseButton;
        [SerializeField] private Button randomHouseButton;
        [Space]
        [SerializeField] private Button backButton;
        [Space]
        [SerializeField] private DemoSelectorBoardPresenter presenter;
        
        public override DisplayBoardsMenu DisplayBoardsMenu => DisplayBoardsMenu.DemoSelector;

        private void Start()
        {
            firstHouseButton.onClick.AddListener(presenter.OnFirstHouseClicked);
            apartmentButton.onClick.AddListener(presenter.OnApartmentHouseClicked);
            smallHouseButton.onClick.AddListener(presenter.OnSmallHouseClicked);
            randomHouseButton.onClick.AddListener(presenter.OnRandomHouseClicked);
            
            backButton.onClick.AddListener(presenter.OnBackButtonClicked);
        }

        private void OnDestroy()
        {
            firstHouseButton.onClick.RemoveListener(presenter.OnFirstHouseClicked);
            apartmentButton.onClick.RemoveListener(presenter.OnApartmentHouseClicked);
            smallHouseButton.onClick.RemoveListener(presenter.OnSmallHouseClicked);
            randomHouseButton.onClick.RemoveListener(presenter.OnRandomHouseClicked);
            
            backButton.onClick.RemoveListener(presenter.OnBackButtonClicked);
        }
    }
}