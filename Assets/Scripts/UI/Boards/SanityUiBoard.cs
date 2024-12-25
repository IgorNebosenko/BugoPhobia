using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ElectrumGames.Core.Player;
using ElectrumGames.UI.Components;
using TMPro;
using UnityEngine;
using Zenject;

namespace ElectrumGames.UI.Boards
{
    public class SanityUiBoard : MonoBehaviour
    {
        [SerializeField] private string averageSanityPatent = "Average sanity: {0}%";
        [SerializeField] private float updatePauseTime = 1f;
        [Space]
        [SerializeField] private TMP_Text headerText;
        [SerializeField] private Transform sanityComponentsRoot;
        [SerializeField] private SanityComponent sanityComponentTemplate;

        private List<SanityComponent> _sanityComponents;
        private MissionPlayersHandler _playersHandler;

        [Inject]
        private void Inject(MissionPlayersHandler playersHandler)
        {
            _playersHandler = playersHandler;
        }

        private void Start()
        {
            _sanityComponents = new List<SanityComponent>();
            headerText.text = "Sanity";

            StartCoroutine(UpdateProcess());
        }

        private IEnumerator UpdateProcess()
        {
            yield return new WaitForSeconds(updatePauseTime);
            
            for (var i = 0; i < _playersHandler.Players.Count; i++)
            {
                var playerElement = Instantiate(sanityComponentTemplate, sanityComponentsRoot);
                playerElement.Init(_playersHandler.Players[0], Color.red);
                _sanityComponents.Add(playerElement);
            }
            
            while (true)
            {
                yield return new WaitForSeconds(updatePauseTime);
                
                for (var i = 0; i < _sanityComponents.Count; i++)
                    _sanityComponents[i].UpdateSanity();
                
                headerText.text = string.Format(averageSanityPatent, 
                    (int)_sanityComponents.Average(x => x.CurrentSanity));
            }
        }
    }
}