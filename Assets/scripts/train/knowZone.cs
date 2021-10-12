using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class knowZone : MonoBehaviour, IPointerClickHandler {

    public GameObject canvas;

    private RectTransform rt;
    private void Awake() {
        rt = GetComponent<RectTransform>();
    }
    public void OnPointerClick(PointerEventData eventData) {
        if (!train.isCardSelected || train.chosenCard.GetComponent<cardManager>().isCardOpened)
            return;
        GameObject chosenCard = train.chosenCard;
        GameObject answer = chosenCard.transform.Find("answer").gameObject;

        if (answer != null && answer.activeSelf && answer.GetComponent<TMP_InputField>().text != "") {
            if(answer.GetComponent<TMP_InputField>().text == Globals.dataBase[train.chosenCard.GetComponent<cardManager>().carryWordInd].translation) {
                int ind = chosenCard.GetComponent<cardManager>().carryWordInd;
                Globals.dataBase[ind].refreshTime();
                Globals.SaveWordData();

                Vector2 pos = eventData.position;
                pos = ScaleSystem.scalePosToCanvas(pos, canvas.GetComponent<RectTransform>());
                pos = new Vector2(pos.x - canvas.GetComponent<RectTransform>().rect.width, pos.y - canvas.GetComponent<RectTransform>().rect.height);

                Vector2 cardSizes = new Vector2(chosenCard.GetComponent<RectTransform>().rect.width, chosenCard.GetComponent<RectTransform>().rect.width);

                pos = new Vector2(Mathf.Clamp(pos.x, -(canvas.GetComponent<RectTransform>().rect.width) + (cardSizes.x / 4), -cardSizes.x / 4), Mathf.Clamp(pos.y, rt.anchoredPosition.y - (rt.rect.height / 2) + (cardSizes.y / 2),   rt.anchoredPosition.y + (rt.rect.height / 2) - (cardSizes.y / 2)));

                chosenCard.GetComponent<cardManager>().answerInputField.SetActive(false);
                chosenCard.GetComponent<cardManager>().targetPosition = pos;
                chosenCard.GetComponent<cardManager>().targetSize /= 2;
                train.isCardSelected = false;
                train.chosenCard = null;
            }
            else {
                chosenCard.GetComponent<cardManager>().ShowTranslationSide();
            }
        }
    }
}
