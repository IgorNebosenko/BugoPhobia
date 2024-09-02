using System;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Components
{
    public class SettingsButtonItem : SettingsItemBase
    {
        [SerializeField] private Button buttonChange;
        [SerializeField] private Text textButton;

        private Action _cashedEvent;
        private bool _isInited;
        
        public void Init(string tileName, string keyName, Action buttonAction)
        {
            nameText.text = tileName;
            
            textButton.text = keyName;
            _cashedEvent = buttonAction;
            
            buttonChange.onClick.AddListener(_cashedEvent.Invoke);
            
            _isInited = true;
        }

        private void OnDestroy()
        {
            if (_isInited)
                return;
            
            buttonChange.onClick.RemoveListener(_cashedEvent.Invoke);
        }
    }
}