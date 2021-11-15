using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class forgotZone : MonoBehaviour, IPointerClickHandler {

    public GameObject canvas;

    private RectTransform rt;
    private void Awake() {
        rt = GetComponent<RectTransform>();
    }
    public void OnPointerClick(PointerEventData eventData) {
        if (!train.isCardSelected)
            return;
        cardManager chosenCard = train.chosenCard.GetComponent<cardManager>();
        if (!chosenCard.isCardOpened) {
            chosenCard.ShowTranslationSide();
            SaveSystem.SaveWordData(Settings.curLang);
        }
        else {
            Vector2 pos = eventData.position;
            pos = ScaleSystem.scalePosToCanvas(pos, canvas.GetComponent<RectTransform>());
            pos = new Vector2(pos.x - (canvas.GetComponent<RectTransform>().rect.width), pos.y - (canvas.GetComponent<RectTransform>().rect.height));

            Vector2 cardSizes = new Vector2(chosenCard.GetComponent<RectTransform>().rect.width, chosenCard.GetComponent<RectTransform>().rect.width);
            pos = new Vector2(Mathf.Clamp(pos.x, -(canvas.GetComponent<RectTransform>().rect.width) + (cardSizes.x / 4), -cardSizes.x / 4), 
                              Mathf.Clamp(pos.y, (cardSizes.y / 2) - canvas.GetComponent<RectTransform>().rect.height, rt.anchoredPosition.y + (rt.rect.height / 2) - (canvas.GetComponent<RectTransform>().rect.height / 2) - (cardSizes.y / 2)));


            chosenCard.ShowWordSide();
            chosenCard.targetPosition = pos;
            chosenCard.targetSize /= 2;
            train.isCardSelected = false;
            train.chosenCard = null;
            train.leaveButton.interactable = true;
        }
    }
}
