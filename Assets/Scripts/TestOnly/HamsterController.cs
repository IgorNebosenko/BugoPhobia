using ElectrumGames.Core.Lobby;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ElectrumGames
{
    public class HamsterController : MonoBehaviour
    {
        [SerializeField] private Button hamsterButton;
        
        private MoneysHandler _moneysHandler;
        
        [Inject]
        private void Construct(MoneysHandler moneysHandler)
        {
            _moneysHandler = moneysHandler;
            
            hamsterButton.onClick.AddListener(() => _moneysHandler.Moneys++);
        }
    }
}
