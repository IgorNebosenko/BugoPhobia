using System.Linq;
using System.Text;
using ElectrumGames.Configs;
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

        private void Start()
        {
            foreach (var config in Presenter.DescriptionConfig.Data)
            {
                var evidenceConfig = Presenter.EvidenceConfig.ConfigData.First
                    (x => x.GhostType == config.GhostType);
                
                Instantiate(buttonTemplate, ghostsListTransform).Init(config.Name, 
                    () => SetDescription(config, evidenceConfig));
            }
        }

        private void OnDestroy()
        {
            
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
    }
}