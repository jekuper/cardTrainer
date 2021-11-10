using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class archive : MonoBehaviour {

    public GameObject wordPrefab;
    public GameObject wordListContent;

    public TMP_InputField wordInputField;
    public TMP_InputField translationInputField;
    public TMP_InputField definitionInputField;


    public void UpdateWordList() {

        if (wordListContent == null)
            return;

        string inputWord = wordInputField.text;
        List<word> sortedList = new List<word>(Globals.dataBase);
        sortedList.Sort(delegate (word p1, word p2) {
            int compareDate = p1.fullWord.CompareTo(p2.fullWord);
            return compareDate;
        });
        List<word> resultList = new List<word>();
        foreach (word item in sortedList) {
            if (inputWord == "" || item.fullWord.StartsWith(inputWord)) {
                resultList.Add(item);
            }
        }
        foreach (Transform child in wordListContent.transform) {
            Destroy(child.gameObject);
        }
        foreach (word item in resultList) {
            GameObject newElement = Instantiate(wordPrefab, wordListContent.transform);
            newElement.GetComponent<wordListPrefab>().LoadWord(item);
        }
    }

    public void AddButtonClicked() {
        if (wordInputField.text != "" && translationInputField.text != "") {
            Globals.AddWord(new word(wordInputField.text, new List<string>() { translationInputField.text }, definitionInputField.text));
            wordInputField.text = "";
            translationInputField.text = "";
            definitionInputField.text = "";
            UpdateWordList();
        }
    }

    private void Start() {
        UpdateWordList();
    }

    private void OnApplicationQuit() {
        SaveSystem.SaveWordData(Settings.curLang);
    }
    private void OnApplicationFocus(bool focus) {
        if (!focus) {
            SaveSystem.SaveWordData(Settings.curLang);
        }
    }
}
