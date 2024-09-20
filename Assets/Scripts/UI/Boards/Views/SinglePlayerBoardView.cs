using ElectrumGames.UI.Boards.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Boards.Views
{
    public class SinglePlayerBoardView : BoardViewBase
    {
        [SerializeField] private TMP_Text balanceText;
        [SerializeField] private string balanceFormat;
        [Space]
        [SerializeField] private Button difficultyButton;
        [SerializeField] private TMP_Text difficultyText;
        [Space]
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private string levelFormat;
        [Space]
        [SerializeField] private TMP_Text percentText;
        [SerializeField] private string percentFormat;
        [SerializeField] private Image percentImage;
        [Space]
        [SerializeField] private TMP_Text experienceText;
        [SerializeField] private string experienceFormat;
        [Space]
        [SerializeField] private Button startButton;
        [Space]
        [SerializeField] private TMP_Text inventoryText;
        [Space]
        [SerializeField] private EquipmentItemComponent equipmentItemTemplate;
        [Space]
        [SerializeField] private SinglePlayerBoardPresenter presenter;

        public override DisplayBoardsMenu DisplayBoardsMenu => DisplayBoardsMenu.SinglePlayer;
    }
}