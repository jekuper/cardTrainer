using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class train : MonoBehaviour
{
    public GameObject[] cards;
    public static bool isCardSelected = false;
    public static GameObject chosenCard = null;
    private void Start() {
        refreshCards();
    }

    public void refreshCards() {
        if (isCardSelected)
            return;
        List<int> te = new List<int>();
        for (int i = 0; i < Globals.dataBase.Count; i++) {
            te.Add(i);
        }
        te.Sort((x, y) => Globals.dataBase[x].time.CompareTo(Globals.dataBase[y].time));

        List<int> res = new List<int>();
        for (int i = 0; i < Mathf.Min(te.Count, 15); i++) {
            res.Add(te[i]);
        }

        for (int i = 0; i < res.Count; i++) {
            int temp = res[i];
            int randomIndex = Random.Range(i, res.Count);
            res[i] = res[randomIndex];
            res[randomIndex] = temp;
        }

        for (int i = 0; i < 10; i++) {
            if (i >= cards.Length || cards[i] == null)
                continue;
            cardManager cm = cards[i].GetComponent<cardManager>();
            cm.answerInputField.SetActive(false);
            cm.answerInputField.GetComponent<TMP_InputField>().text = "";
            cm.targetPosition = cm.startPosition;
            cm.isCardUsed = false;
            cm.isCardChoosed = false;
            cm.carryWordInd = res[i];
            cm.LoadWord();
            cm.ShowWordSide();
            909
        }
    }
}
