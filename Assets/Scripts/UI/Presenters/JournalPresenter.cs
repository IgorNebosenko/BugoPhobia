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
            Cursor.visible = true;
            _inputActions.Player.Disable();
        }

        protected override void Closing()
        {
            Cursor.visible = false;
            _inputActions.Player.Enable();
        }

        public bool CalculateGhostState(GhostType ghost)
        {
            var ghostData = EvidenceConfig.ConfigData.First(x => x.GhostType == ghost);

            return JournalManager.PlayerJournalInstance.SelectedEvidences.All(
                x => ghostData.Evidences.Contains(x) && 
                     JournalManager.PlayerJournalInstance.DeselectedEvidences.All(
                         x => !ghostData.Evidences.Contains(x)));
        }
    }
}