using ElectrumGames.Configs;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.MVP;
using ElectrumGames.UI.Components.Enums;
using ElectrumGames.UI.Views;
using UnityEngine;

namespace ElectrumGames.UI.Presenters
{
    public class JournalPresenter : Presenter<JournalView>
    {
        private readonly InputActions _inputActions;
        public DescriptionConfig DescriptionConfig { get; }
        public EvidenceConfig EvidenceConfig { get; }
        
        public JournalPresenter(InputActions inputActions, DescriptionConfig descriptionConfig,
            EvidenceConfig evidenceConfig, JournalView view) : base(view)
        {
            _inputActions = inputActions;
            DescriptionConfig = descriptionConfig;
            EvidenceConfig = evidenceConfig;
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

        public void OnUltravioletStateChanged(UiJournalElementState state)
        {
        }

        public void OnRadiationStateChanged(UiJournalElementState state)
        {
        }

        public void OnGhostWritingStateChanged(UiJournalElementState state)
        {
        }

        public void OnTorchingStateChanged(UiJournalElementState state)
        {
        }

        public void OnFreezingTemperatureStateChanged(UiJournalElementState state)
        {
        }

        public void OnEMF5StateChanged(UiJournalElementState state)
        {
        }

        public void OnSpiritBoxStateChanged(UiJournalElementState state)
        {
        }
    }
}