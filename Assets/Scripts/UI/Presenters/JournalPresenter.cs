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

        public void OnUltravioletStateChanged(EvidenceState state)
        {
        }

        public void OnRadiationStateChanged(EvidenceState state)
        {
        }

        public void OnGhostWritingStateChanged(EvidenceState state)
        {
        }

        public void OnTorchingStateChanged(EvidenceState state)
        {
        }

        public void OnFreezingTemperatureStateChanged(EvidenceState state)
        {
        }

        public void OnEMF5StateChanged(EvidenceState state)
        {
        }

        public void OnSpiritBoxStateChanged(EvidenceState state)
        {
        }
    }
}