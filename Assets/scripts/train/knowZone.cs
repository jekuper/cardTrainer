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
        
        cardManager chosenCard = train.chosenCard.GetComponent<cardManager>();
        GameObject answer = chosenCard.transform.Find("answer").gameObject;

        int ind = chosenCard.carryWordInd;
        if (answer != null && answer.activeSelf && answer.GetComponent<TMP_InputField>().text != "") {
            if((!chosenCard.isCardInputReversed && Globals.dataBase[ind].checkAnswer(answer.GetComponent<TMP_InputField>().text)) ||
                (chosenCard.isCardInputReversed && Globals.dataBase[ind].fullWord == answer.GetComponent<TMP_InputField>().text)) {
                Globals.dataBase[ind].refreshTime();
                Globals.dataBase[ind].statistic.Add(1);
                SaveSystem.SaveWordData(Settings.curLang);

                Vector2 pos = eventData.position;
                pos = ScaleSystem.scalePosToCanvas(pos, canvas.GetComponent<RectTransform>());
                pos = new Vector2(pos.x - canvas.GetComponent<RectTransform>().rect.width, pos.y - canvas.GetComponent<RectTransform>().rect.height);

                Vector2 cardSizes = new Vector2(chosenCard.GetComponent<RectTransform>().rect.width, chosenCard.GetComponent<RectTransform>().rect.width);

                pos = new Vector2(Mathf.Clamp(pos.x, -(canvas.GetComponent<RectTransform>().rect.width) + (cardSizes.x / 4), -cardSizes.x / 4), Mathf.Clamp(pos.y, rt.anchoredPosition.y - (rt.rect.height / 2) + (cardSizes.y / 2),   rt.anchoredPosition.y + (rt.rect.height / 2) - (cardSizes.y / 2)));

                chosenCard.answerInputField.SetActive(false);
                chosenCard.targetPosition = pos;
                chosenCard.targetSize /= 2;
                train.isCardSelected = false;
                train.chosenCard = null;
                train.leaveButton.interactable = true;
            }
            else {
                chosenCard.ShowTranslationSide();
            }
        }
    }
}
