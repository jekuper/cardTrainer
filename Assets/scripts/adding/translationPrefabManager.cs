using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class translationPrefabManager : MonoBehaviour
{
    public int trInd;
    public bool isWordLoaded = false;
    public TMP_InputField input;
    public Button deleteButton;

    private editorTranslationManager manager;
    private void Start() {
        manager = transform.parent.parent.parent.GetComponent<editorTranslationManager>();
        if (manager == null)
            Debug.LogWarning("manager not found on parent!");
    }
    public void DeleteSelf() {
        if (manager.copy.Count <= 1)
            return;
        manager.copy.RemoveAt(trInd);
        manager.InitList();
    }
    public void Deselect() {
        if (input.text == "") {
            DeleteSelf();
            return;
        }
        if (isWordLoaded) {
            manager.copy[trInd] = input.text;
        }
        else {
            if(!manager.copy.Contains(word.SanitizeString(input.text)))
                manager.copy.Add(word.SanitizeString(input.text));
        }
        manager.InitList();
    }
    public void LoadWord(string tr, int ind) {
        trInd = ind;
        input.text = word.decrypt(tr);
        isWordLoaded = true;
        deleteButton.interactable = true;
    }
}
