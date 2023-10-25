using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public class trainRecord {
    public int totalWords = 10;
    public int forgotten = 0;
    public DateTime date = DateTime.Now;

    public trainRecord (int _forgotten, int _total = 10) {
        totalWords = _total;
        forgotten = _forgotten;
    }
}

public class train : MonoBehaviour
{
    public GameObject[] cards;
    public Button leaveButtonSnipper;
    public pushReminder reminder;
    public static Button leaveButton;
    public static bool isCardSelected = false;
    public static GameObject chosenCard = null;
    public static int cardsLeft = 10;
    public static train singleton;

    public static int forgottenCount = 0;

    private void Start() {
        if (singleton != null) {
            Destroy (gameObject);
            return;
        }
        singleton = this;
        cardsLeft = 10;
        isCardSelected = false;
        chosenCard = null;

        refreshCards();
        leaveButton = leaveButtonSnipper;
    }

    public void refreshCards() {
        if (isCardSelected || (cardsLeft != 10 && cardsLeft != 0))
            return;

        cardsLeft = 10;
        forgottenCount = 0;

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
    public void ShowGameStats () {
        float score = (10f - forgottenCount) / 10f * 100;
        reminder.ShowReminder ("Contratulations!\nYour score:\n"+score);
        SaveSystem.UpdateTrainStatistics (new trainRecord(forgottenCount, 10));
    }
}
