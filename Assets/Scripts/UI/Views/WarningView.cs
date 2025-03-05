using System.Collections;
using DG.Tweening;
using ElectrumGames.MVP;
using ElectrumGames.UI.Presenters;
using TMPro;
using UnityEngine;

namespace ElectrumGames.UI.Views
{
    [AutoRegisterView]
    public class WarningView : View<WarningPresenter>
    {
        [SerializeField] private float showTime;
        [SerializeField] private float alphaChangeTime;
        [Space]
        [SerializeField] private TMP_Text[] textes;

        private void Start()
        {
            for (var i = 0; i < textes.Length; i++)
            {
                textes[i].alpha = 0f;

                var j = i;
                var color = textes[j].color;

                DOTween.To(() => textes[j].color, x => textes[j].color = x,
                        new Color(color.r, color.g, color.b, 1f), alphaChangeTime)
                    .OnComplete(() => StartCoroutine(AwaitProcess()));
            }
        }

        private IEnumerator AwaitProcess()
        {
            yield return new WaitForSeconds(showTime);
            
            for (var i = 0; i < textes.Length; i++)
            {
                var j = i;
                var color = textes[j].color;

                DOTween.To(() => textes[j].color, x => textes[j].color = x,
                        new Color(color.r, color.g, color.b, 0f), alphaChangeTime)
                    .OnComplete(() => Presenter.ShowNextView());
            }
        }
    }
}