using ElectrumGames.MVP;
using ElectrumGames.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Views
{
    [AutoRegisterView("Views/Popups/SpiritBoxPopup")]
    public class SpiritBoxPopup : View<SpiritBoxPopupPresenter>
    {
        [SerializeField] private Button whereAreYouButton;
        [SerializeField] private Button areYouMaleButton;
        [SerializeField] private Button ageButton;
        [Space]
        [SerializeField] private Button closeButton;
    }
}