using System.Linq;
using ElectrumGames.Configs;
using ElectrumGames.Core.Journal;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.GlobalEnums;
using ElectrumGames.MVP;
using ElectrumGames.UI.Views;
using UnityEngine;

namespace ElectrumGames.UI.Presenters
{
    public class JournalPresenter : Presenter<JournalView>
    {
        private readonly InputActions _inputActions;
        public DescriptionConfig DescriptionConfig { get; }
        public EvidenceConfig EvidenceConfig { get; }
        public JournalManager JournalManager { get; }
        
        public JournalPresenter(InputActions inputActions, DescriptionConfig descriptionConfig,
            EvidenceConfig evidenceConfig, JournalManager journalManager, JournalView view) : base(view)
        {
            _inputActions = inputActions;
            DescriptionConfig = descriptionConfig;
            EvidenceConfig = evidenceConfig;
            JournalManager = journalManager;
        }
        
        protected override void Init()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _inputActions.Moving.Disable();
            _inputActions.Interactions.Disable();
        }

        protected override void Closing()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _inputActions.Moving.Enable();
            _inputActions.Interactions.Enable();
        }

        public bool CalculateGhostState(GhostType ghost)
        {
            var ghostData = EvidenceConfig.ConfigData.First(x => x.GhostType == ghost);

            var selectedEvidences = JournalManager.PlayerJournalInstance.SelectedEvidences;
            var deselectedEvidences = JournalManager.PlayerJournalInstance.DeselectedEvidences;

            return (selectedEvidences.Count == 0 || selectedEvidences.All(x => ghostData.Evidences.Contains(x))) &&
                   deselectedEvidences.All(x => !ghostData.Evidences.Contains(x));
        }
    }
}