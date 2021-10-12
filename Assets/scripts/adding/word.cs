using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class word
{
    public string fullWord;
    public string definition;
    public string translation;
    public DateTime time;
    public word() {}

    public string SanitizeString(string str) {
        str = str.Replace("-", "#dash#");
        str = str.Replace("\n", "#newLine#");
        return str;
    }
    public word(string _fullWord, string _translation, string _definition = "") {
        fullWord = SanitizeString(_fullWord);
        translation = SanitizeString(_translation);
        definition = SanitizeString(_definition);
        refreshTime();
    }
    public word(string oneString) {
        string[] dt = oneString.Split('-');
        fullWord = dt[0];
        translation = dt[1];
        definition = dt[2];
        time = DateTime.ParseExact(dt[3], "G", null);
    }
    public string toOneString() {
        string ans = "";
        ans += fullWord;
        ans += "-";
        ans += translation;
        ans += "-";
        ans += definition;
        ans += "-";
        ans += time.ToString("G");
        return ans;
    }
    public void refreshTime() {
        time = DateTime.Now;
    }
    public static string decrypt(string data) {
        data = data.Replace("#dash#", "-");
        data = data.Replace("#newLine#", "\n");
        return data;
    }

    public void LoadToPrefab(GameObject prefab) {
        GameObject orig, trans;
        prefab.GetComponent<wordListPrefab>().word = this;
        Transform container = prefab.transform.Find("container");
        orig = container.Find("word").gameObject;
        trans = container.Find("translation").gameObject;

        orig.GetComponent<TextMeshProUGUI>().text = fullWord;
        trans.GetComponent<TextMeshProUGUI>().text = translation;
    }
}
