using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class train : MonoBehaviour
{
    public GameObject[] cards;
    public Button leaveButtonSnipper;
    public static Button leaveButton;
    public static bool isCardSelected = false;
    public static GameObject chosenCard = null;
    private void Start() {
        refreshCards();
        leaveButton = leaveButtonSnipper;
    }

    public void refreshCards() {
        if (isCardSelected)
            return;

        List<int> res = Globals.sortByStatistic(15);
        

        for (int i = 0; i < 10; i++) {
            if (i >= cards.Length || cards[i] == null)
                continue;
            cardManager cm = cards[i].GetComponent<cardManager>();
            cm.answerInputField.SetActive(false);
            cm.answerInputField.GetComponent<TMP_InputField>().text = "";
            cm.targetPosition = cm.startPosition;
            cm.isCardUsed = false;
            cm.isCardChoosed = false;
            cm.isCardInputReversed = (Globals.dataBase[res[i]].translationScore < Globals.dataBase[res[i]].fullWordScore);
            cm.carryWordInd = res[i];
            cm.wordTranslationInd = -1;
            cm.LoadWord();
            cm.ShowWordSide();
        }
    }
}
