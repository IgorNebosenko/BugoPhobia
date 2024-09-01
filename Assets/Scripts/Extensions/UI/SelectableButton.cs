using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ElectrumGames.Extensions.UI
{
    public class SelectableButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private Image buttonImage;

        [SerializeField] private Sprite pointerDownSprite;
        [SerializeField] private Sprite pointerUpSprite;
        
        
        public void OnPointerDown(PointerEventData eventData)
        {
            buttonImage.sprite = pointerDownSprite;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buttonImage.sprite = pointerUpSprite;
        }
    }
}