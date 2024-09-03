using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Components
{
    public class GhostsJournalButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text textObject;

        private Action _cashedEvent;

        private bool _isInited;
        
        public void Init(string text, Action onClick)
        {
            textObject.text = text;
            _cashedEvent = onClick;
            
            button.onClick.AddListener(() => _cashedEvent?.Invoke());
            
            _isInited = true;
        }

        private void OnDestroy()
        {
            if (!_isInited)
                return;
            
            button.onClick.RemoveListener(() => _cashedEvent?.Invoke());
        }
    }
}