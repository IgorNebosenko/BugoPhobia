﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElectrumGames.Configs;
using ElectrumGames.Core.Journal;
using ElectrumGames.GlobalEnums;
using ElectrumGames.MVP;
using ElectrumGames.UI.Components;
using ElectrumGames.UI.Presenters;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Views
{
    [AutoRegisterView]
    public class JournalView : View<JournalPresenter>
    {
        [SerializeField, FoldoutGroup("Header")] private Button ghostsButton;
        [SerializeField, FoldoutGroup("Header")] private Button evidencesButton;
        
        [SerializeField, FoldoutGroup("Ghosts")] private GhostsJournalButton buttonTemplate;
        [SerializeField, FoldoutGroup("Ghosts")] private Transform ghostsListTransform;
        [Space]
        [SerializeField, FoldoutGroup("Ghosts")] private TMP_Text ghostDescriptionText;
        [SerializeField, FoldoutGroup("Ghosts")] private TMP_Text ghostStrengthText;
        [SerializeField, FoldoutGroup("Ghosts")] private TMP_Text ghostWeaknessesText;
        [Space]
        [SerializeField, FoldoutGroup("Ghosts")] private TMP_Text ghostsEvidencesText;

        [SerializeField, FoldoutGroup("Evidences")] private EvidenceUiElement evidenceElementTemplate;
        [SerializeField, FoldoutGroup("Evidences")] private Transform evidenceListTransform;
        [Space]
        [SerializeField, FoldoutGroup("Evidences")] private GhostUiElement ghostElementTemplate;
        [SerializeField, FoldoutGroup("Evidences")] private Transform ghostListTransform;
        
        [SerializeField, FoldoutGroup("Tabs")] private GameObject ghostsTab;
        [SerializeField, FoldoutGroup("Tabs")] private GameObject evidencesTab;
        
        private List<Action> _listGhostsEvents;
        private List<GhostUiElement> _ghostUiElements;

        private void Start()
        {
            for (var i = 0; i < Presenter.DescriptionConfig.Data.Length; i++)
            {
                var j = i;
                
                var evidenceConfig = Presenter.EvidenceConfig.ConfigData.First
                    (x => x.GhostType == Presenter.DescriptionConfig.Data[j].GhostType);
                
                Instantiate(buttonTemplate, ghostsListTransform).Init(
                    $"{i + 1}. {Presenter.DescriptionConfig.Data[j].Name}", 
                    () => SetDescription(Presenter.DescriptionConfig.Data[j], evidenceConfig));
            }
            
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Ultraviolet", 
                Presenter.JournalManager.PlayerJournalInstance.GetEvidenceState(EvidenceType.UV), 
                element => Presenter.JournalManager.PlayerJournalInstance.HandleEvidence(EvidenceType.UV, element));
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Radiation", 
                Presenter.JournalManager.PlayerJournalInstance.GetEvidenceState(EvidenceType.Radiation),
                element => Presenter.JournalManager.PlayerJournalInstance.HandleEvidence(EvidenceType.Radiation, element));
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Ghost writing", 
                Presenter.JournalManager.PlayerJournalInstance.GetEvidenceState(EvidenceType.GhostWriting),
                element => Presenter.JournalManager.PlayerJournalInstance.HandleEvidence(EvidenceType.GhostWriting, element));
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Torching", 
                Presenter.JournalManager.PlayerJournalInstance.GetEvidenceState(EvidenceType.Torching), 
                element => Presenter.JournalManager.PlayerJournalInstance.HandleEvidence(EvidenceType.Torching, element));
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Freezing temperature", 
                Presenter.JournalManager.PlayerJournalInstance.GetEvidenceState(EvidenceType.FreezingTemperature), 
                element => Presenter.JournalManager.PlayerJournalInstance.HandleEvidence(EvidenceType.FreezingTemperature, element));
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("EMF 5", 
                Presenter.JournalManager.PlayerJournalInstance.GetEvidenceState(EvidenceType.EMF5), 
                element => Presenter.JournalManager.PlayerJournalInstance.HandleEvidence(EvidenceType.EMF5, element));
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Spirit box", 
                Presenter.JournalManager.PlayerJournalInstance.GetEvidenceState(EvidenceType.SpiritBox), 
                element => Presenter.JournalManager.PlayerJournalInstance.HandleEvidence(EvidenceType.SpiritBox, element));
            
            _listGhostsEvents = new List<Action>();
            _ghostUiElements = new List<GhostUiElement>();
            
            foreach (var ghost in Presenter.DescriptionConfig.Data)
            {
                var ghostElement = Instantiate(ghostElementTemplate, ghostListTransform);
                ghostElement.Init(ghost.Name, Presenter.JournalManager.PlayerJournalInstance.GetUserGhostState(ghost.GhostType), 
                    Presenter.CalculateGhostState(ghost.GhostType), 
                    element => OnJournalItemStateUpdated(element, ghost.GhostType), ghost.GhostType);
                
                Action action = () => ghostElement.SetState(Presenter.CalculateGhostState(ghost.GhostType));
                Presenter.JournalManager.PlayerJournalInstance.JournalUpdated += action;
                _listGhostsEvents.Add(action);
                
                _ghostUiElements.Add(ghostElement);
            }
            
            ghostsButton.onClick.AddListener(() => SwitchTab(true));
            evidencesButton.onClick.AddListener(() => SwitchTab(false));
            
            SwitchTab(false);
        }

        private void OnDestroy()
        {
            ghostsButton.onClick.RemoveListener(() => SwitchTab(true));
            evidencesButton.onClick.RemoveListener(() => SwitchTab(false));

            foreach (var eventItem in _listGhostsEvents)
            {
                Presenter.JournalManager.PlayerJournalInstance.JournalUpdated -= eventItem;
            }
        }

        private void OnJournalItemStateUpdated(JournalItemState state, GhostType type)
        {
            Presenter.JournalManager.PlayerJournalInstance.HandleGhost(type, state);

            foreach (var element in _ghostUiElements)
            {
                state = Presenter.JournalManager.PlayerJournalInstance.GetUserGhostState(element.GhostType);
                element.UpdateSelection(state);
            }
        }

        private void SetDescription(DescriptionConfigData data, EvidenceConfigData evidenceData)
        {
            ghostDescriptionText.text = data.Description;
            ghostStrengthText.text = $"Strength: {data.Strength}";
            ghostWeaknessesText.text = $"Weaknesses: {data.Weaknesses}";

            var sb = new StringBuilder("Evidences: \n");
            for (var i = 0; i < evidenceData.Evidences.Length; i++)
            {
                sb.Append("- ");
                sb.Append(evidenceData.Evidences[i]);

                if (i == 0 && evidenceData.IsFirstMandatory)
                    sb.Append("* (Mandatory)");
                
                sb.Append("\n");
            }
            
            ghostsEvidencesText.text = sb.ToString();
        }

        private void SwitchTab(bool isGhosts)
        {
            ghostsTab.SetActive(isGhosts);
            evidencesTab.SetActive(!isGhosts);
        }
    }
}