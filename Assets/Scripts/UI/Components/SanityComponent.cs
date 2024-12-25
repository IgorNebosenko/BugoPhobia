using ElectrumGames.Core.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Components
{
    public class SanityComponent : MonoBehaviour
    {
        [SerializeField] private TMP_Text playerName;
        [Space]
        [SerializeField] private Image sanityProgressbar;
        [SerializeField] private TMP_Text sanityPercent;
        [Space]
        [SerializeField] private float differencePercent = 5f;

        public int CurrentSanity { get; private set; }

        private IPlayer _player;
        
        public void Init(IPlayer player, Color progressColor)
        {
            _player = player;

            playerName.text = player.IsPlayablePlayer ? "You" : "CrewMate";
            sanityProgressbar.color = progressColor;
        }

        public void UpdateSanity()
        {
            CurrentSanity = (int)Mathf.Clamp(_player.Sanity.CurrentSanity + Random.Range(
                -differencePercent, differencePercent), 0, 100);

            sanityProgressbar.fillAmount = CurrentSanity / 100f;
            sanityPercent.text = $"{CurrentSanity}%";
        }
    }
}