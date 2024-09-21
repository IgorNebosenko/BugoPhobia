using System;
using System.Collections.Generic;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Items;
using ElectrumGames.GlobalEnums;
using ElectrumGames.UI.Boards.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
        [SerializeField] private Button backButton;
        [SerializeField] private Button startButton;
        [Space]
        [SerializeField] private Transform inventoryObject;
        [Space]
        [SerializeField] private EquipmentItemComponent equipmentItemTemplate;
        [Space]
        [SerializeField] private SinglePlayerBoardPresenter presenter;

        private List<EquipmentItemComponent> equipmentItems = new ();

        private ItemsConfig _itemsConfig;
        
        public override DisplayBoardsMenu DisplayBoardsMenu => DisplayBoardsMenu.SinglePlayer;

        [Inject]
        private void ItemsConfig(ItemsConfig itemsConfig)
        {
            _itemsConfig = itemsConfig;
        }

        private void OnEnable()
        {
            backButton.onClick.AddListener(presenter.OnButtonBackClicked);
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveListener(presenter.OnButtonBackClicked);
        }

        private void Start()
        {
            SetBalanceText(presenter.MoneysHandler.Moneys);
            presenter.MoneysHandler.BalanceUpdated += SetBalanceText;
            
            SetLevelText(134);
            SetProgress(374, 7575);
            
            Debug.LogError("Here must be load config of default inventory for mission");
            
            SetItemsList(presenter.LobbyItemsHandler.GetSortedList());
        }

        public void SetBalanceText(decimal balance)
        {
            balance = Math.Floor(balance);
            
            balanceText.text = string.Format(balanceFormat, balance);
        }

        public void SetLevelText(int level)
        {
            levelText.text = string.Format(levelFormat, level);
        }

        public void SetProgress(float currentXp, float maxXp)
        {
            var percent = Mathf.RoundToInt(currentXp / maxXp * 100f);
            
            percentText.text = string.Format(percentFormat, percent);
            percentImage.fillAmount = currentXp / maxXp;

            experienceText.text = string.Format(experienceFormat, currentXp, maxXp);
        }

        public void SetItemsList(List<ItemLobbyData> items)
        {
            foreach (var item in equipmentItems)
            {
                Destroy(item.gameObject);
            }
            
            equipmentItems.Clear();

            for (var i = 0; i < items.Count; i++)
            {
                var item = Instantiate(equipmentItemTemplate, inventoryObject);
                item.OnInit(_itemsConfig, items[i].itemType, items[i].currentCount, items[i].maxCount);
                equipmentItems.Add(item);
            }
        }
    }
}