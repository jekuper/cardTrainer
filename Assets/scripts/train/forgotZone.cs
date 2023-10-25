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
            RectTransformUtility.ScreenPointToLocalPointInRectangle (canvas.GetComponent<RectTransform>(), eventData.position, Camera.main, out pos);

            chosenCard.ShowWordSide();
            chosenCard.targetSize = new Vector2(122, 200);
            chosenCard.setTargetPositionToTouch (canvas.GetComponent<RectTransform>(), eventData.position, GetComponent<RectTransform>().rect.size.y);
//            chosenCard.targetPosition = pos;
            train.isCardSelected = false;
            train.chosenCard = null;
            train.leaveButton.interactable = true;
            if (train.cardsLeft == 0) {
                train.singleton.ShowGameStats ();
            }
        }
    }
}
