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

        private void Start()
        {
            foreach (var config in Presenter.DescriptionConfig.Data)
            {
                Instantiate(buttonTemplate, ghostsListTransform).Init(config.Name, 
                    () => SetDescription(config));
            }
        }

        private void OnDestroy()
        {
            
        }

        private void SetDescription(DescriptionConfigData data)
        {
            ghostDescriptionText.text = data.Description;
            ghostStrengthText.text = $"Strength: {data.Strength}";
            ghostWeaknessesText.text = $"Weaknesses: {data.Weaknesses}";
        }
    }
}