using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsManager : MonoBehaviour
{

    public static settingsManager singleton;

    public GameObject langPrefab;
    public Transform listContent;
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
        if (singleton != null) {
            Destroy (gameObject);
            return;
        }
        singleton = this;
        UpdateList();
    }
}
