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
            if((!chosenCard.isCardInputReversed && Globals.dataBase[ind].checkAnswerTranslation(answer.GetComponent<TMP_InputField>().text)) ||
                (chosenCard.isCardInputReversed && Globals.dataBase[ind].checkAnswerWord(answer.GetComponent<TMP_InputField>().text))) {
                chosenCard.AcceptCard();
                SaveSystem.SaveWordData(Settings.curLang);

                Vector2 pos = eventData.position;
                RectTransformUtility.ScreenPointToLocalPointInRectangle (canvas.GetComponent<RectTransform> (), eventData.position, Camera.main, out pos);

                chosenCard.answerInputField.SetActive(false);
                chosenCard.targetSize = new Vector2 (122, 200);
                chosenCard.setTargetPositionToTouch (canvas.GetComponent<RectTransform> (), eventData.position, GetComponent<RectTransform> ().rect.size.y, false);
                train.isCardSelected = false;
                train.chosenCard = null;
                train.leaveButton.interactable = true;
                if (train.cardsLeft == 0) {
                    train.singleton.ShowGameStats ();
                }
            }
            else {
                chosenCard.ShowTranslationSide();
                SaveSystem.SaveWordData (Settings.curLang);
            }
        }
    }
}
