using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class word {
    public string fullWord;
    public string definition;
    public List<string> translation = new List<string>();
    public DateTime time;
    public List<int> statistic = new List<int>();

    public word() { }

    public static string SanitizeString(string str) {
        str = str.Replace("-", "#dash#");
        str = str.Replace("\n", "#newLine#");
        str = str.Replace("|", "#upSlash#");
        return str;
    }
    public word(string _fullWord, List<string> _translation, string _definition = "", List<int> _statistic = null) {
        _statistic = _statistic ?? new List<int>();

        fullWord = SanitizeString(_fullWord);
        translation.Clear();
        foreach (string tr in _translation) {
            translation.Add(SanitizeString(tr));
        }
        definition = SanitizeString(_definition);
        statistic.Clear();
        foreach (int item in _statistic) {
            statistic.Add(item);
        }
        refreshTime();
    }
    public word(string oneString) {
        string[] dt = oneString.Split('-');
        fullWord = dt[0];
        translation = new List<string>(dt[1].Split('|'));
        translation.RemoveAt(translation.Count - 1);
        definition = dt[2];

        foreach (string item in dt[3].Split('|')) {
            if (item != "")
                statistic.Add(int.Parse(item));
        }
        time = DateTime.ParseExact(dt[4], "G", null);
    }
    public string toOneString() {
        string ans = "";
        ans += fullWord;
        ans += "-";
        foreach (var tr in translation) {
            ans += tr + "|";
        }
        ans += "-";
        ans += definition;
        ans += "-";
        foreach (int item in statistic) {
            ans += item.ToString() + "|";
        }
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
        data = data.Replace("#upSlash#", "|");
        return data;
    }
    public string translationToString() {
        string ans = "";
        foreach (string tr in translation) {
            ans += tr + ", ";
        }
        if (ans.Length > 1)
            ans = ans.Remove(ans.Length - 2, 2);
        return ans;
    }
    public bool checkAnswer(string inputTranslation) {
        foreach (string tr in this.translation) {
            if (inputTranslation == tr)
                return true;
        }
        return false;
    }

    
}
