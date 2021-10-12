using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordListPrefab : MonoBehaviour
{
    public word word;
    public void clicked() {
        GameObject menu = GameObject.Find("editorMenu");
        if (menu == null)
            return;
        menu.GetComponent<editor>().ShowEditor(word);
    }
}
