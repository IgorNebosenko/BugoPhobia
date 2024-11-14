using System.Collections;
using DG.Tweening;
using ElectrumGames.MVP;
using ElectrumGames.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElectrumGames.UI.Views
{
    [AutoRegisterView]
    public class HeadPhonesView : View<HeadPhonesPresenter>
    {
        [SerializeField] private float showTime;
        [SerializeField] private float alphaChangeTime;
        [Space]
        [SerializeField] private TMP_Text headerText;
        [SerializeField] private Image headphonesIcon;

        private void Start()
        {
            headerText.alpha = 0f;
            var textColor = headerText.color;

            DOTween.To(() => headerText.color, x => headerText.color = x,
                new Color(textColor.r, textColor.g, textColor.b, 1f), alphaChangeTime).
                OnComplete(() => StartCoroutine(AwaitProcess()));

            var imageColor = headphonesIcon.color;
            
            DOTween.To(() => headphonesIcon.color, x => headphonesIcon.color = x,
                new Color(imageColor.r, imageColor.g, imageColor.b, 1f), alphaChangeTime);
        }

        private IEnumerator AwaitProcess()
        {
            yield return new WaitForSeconds(showTime);
            
            var textColor = headerText.color;

            DOTween.To(() => headerText.color, x => headerText.color = x,
                    new Color(textColor.r, textColor.g, textColor.b, 0f), alphaChangeTime).
                OnComplete(() => Presenter.ShowNextView());

            var imageColor = headphonesIcon.color;
            
            DOTween.To(() => headphonesIcon.color, x => headphonesIcon.color = x,
                new Color(imageColor.r, imageColor.g, imageColor.b, 0f), alphaChangeTime);
        }
    }
}