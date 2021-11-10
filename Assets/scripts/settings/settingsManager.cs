using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsManager : MonoBehaviour
{
    public GameObject langPrefab;
    public Transform listContent;
    public void SaveSettings() {
        SaveSystem.SaveSettingsData();
        GetComponent<sceneCameraManager>().ChangeScene("startMenu");
    }
    public void UpdateList() {
        foreach (Transform child in listContent) {
            Destroy(child.gameObject);
        }
        foreach (string lang in Settings.avLangs) {
            Instantiate(langPrefab, listContent).GetComponent<languagePrefabManager>().Load(lang);
        }
        Instantiate(langPrefab, listContent);
    }
    private void Start() {
        UpdateList();
    }
}
