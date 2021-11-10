using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class wordListPrefab : MonoBehaviour
{
    public word word;
    public GameObject orig, trans;
    public void clicked() {
        GameObject menu = GameObject.Find("editorMenu");
        if (menu == null)
            return;
        menu.GetComponent<editor>().ShowEditor(word);
    }
    public void LoadWord(word w) {
        word = w;
        
        orig.GetComponent<TextMeshProUGUI>().text = w.fullWord;
        trans.GetComponent<TextMeshProUGUI>().text = w.translation[0];
    }
}
