using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editorTranslationManager : MonoBehaviour
{
    public GameObject translationPrefab;
    public Transform content;
    public List<string> copy = new List<string>();
    public void InitList() {
        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }
        int i = 0;
        foreach (string tr in copy) {
            Instantiate(translationPrefab, content).GetComponent<translationPrefabManager>().LoadWord(tr, i);
            i++;
        }
        Instantiate(translationPrefab, content);
    }
}
