using ElectrumGames.Configs;
using ElectrumGames.Core.Player.Movement;
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
    }
}