using System.Linq;
using System.Text;
using ElectrumGames.Configs;
using ElectrumGames.MVP;
using ElectrumGames.UI.Components;
using ElectrumGames.UI.Components.Enums;
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

        private void Start()
        {
            for (var i = 0; i < Presenter.DescriptionConfig.Data.Length; i++)
            {
                var j = i;
                
                var evidenceConfig = Presenter.EvidenceConfig.ConfigData.First
                    (x => x.GhostType == Presenter.DescriptionConfig.Data[j].GhostType);
                
                Instantiate(buttonTemplate, ghostsListTransform).Init(
                    $"{i}. {Presenter.DescriptionConfig.Data[j].Name}", 
                    () => SetDescription(Presenter.DescriptionConfig.Data[j], evidenceConfig));
            }
            
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Ultraviolet", 
                UiJournalElementState.Unselected, Presenter.OnUltravioletStateChanged);
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Radiation", 
                UiJournalElementState.Unselected, Presenter.OnRadiationStateChanged);
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Ghost writing", 
                UiJournalElementState.Unselected, Presenter.OnGhostWritingStateChanged);
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Torching", 
                UiJournalElementState.Unselected, Presenter.OnTorchingStateChanged);
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Freezing temperature", 
                UiJournalElementState.Unselected, Presenter.OnFreezingTemperatureStateChanged);
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("EMF 5", 
                UiJournalElementState.Unselected, Presenter.OnEMF5StateChanged);
            Instantiate(evidenceElementTemplate, evidenceListTransform).Init("Spirit box", 
                UiJournalElementState.Unselected, Presenter.OnSpiritBoxStateChanged);
            
            ghostsButton.onClick.AddListener(() => SwitchTab(true));
            evidencesButton.onClick.AddListener(() => SwitchTab(false));
            
            SwitchTab(true);
        }

        private void OnDestroy()
        {
            ghostsButton.onClick.RemoveListener(() => SwitchTab(true));
            evidencesButton.onClick.RemoveListener(() => SwitchTab(false)); 
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