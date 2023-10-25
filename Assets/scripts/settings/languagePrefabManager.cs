using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class languagePrefabManager : MonoBehaviour
{
    public TMP_InputField input;
    public TextMeshProUGUI text;
    public Button deleteButton, selectButton;

    private settingsManager manager;
    private void Start() {
        manager = GameObject.Find("Main Camera").GetComponent<settingsManager>();
        if (manager == null)
            Debug.LogWarning("settings manager not found on parent!");
    }
    public void Load(string lang) {
        input.text = lang;
        if (lang == Settings.curLang) {
            text.color = new Color(0.99215f, 0, 0);
        }
        deleteButton.interactable = true;
        selectButton.interactable = true;
        input.interactable = false;
    }
    public void DeleteSelf() {
        if (input.interactable)
            return;
        Settings.DeleteLang(input.text);
        manager.UpdateList();
    }
    public void AddNewLang() {
        if (input.text == "")
            return;
        foreach (char c in input.text) {
            if (!((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c == ' ') || (c == ')') || (c == '('))) {
                return;
            }
        }
        Settings.AddLang(input.text);
        manager.UpdateList();
    }
    public void SelectSelf() {
        if (input.interactable)
            return;
        Settings.curLang = input.text;
        SaveSystem.SaveSettingsData ();
        manager.UpdateList();
    }
}
