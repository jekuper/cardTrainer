using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class word {
    public string fullWord;
    public string definition = "";
    public List<string> translation = new List<string>();
    public int remembered = 0;
    public int forgotten = 0;

    public int rememberedTotal = 0;
    public int forgottenTotal = 0;

    public int fullWordScore = 0;
    public int translationScore = 0;

    public word() { }

    public static string SanitizeString(string str) {
        str = str.Replace("-", "#dash#");
        str = str.Replace("\n", "#newLine#");
        str = str.Replace("|", "#upSlash#");
        return str;
    }
    public word(string _fullWord, List<string> _translation, string _definition = "", int rem = 0, int forg = 0, int remTotal = 0, int forgTotal = 0, int fwScore = 0, int trScore = 0) {
        
        fullWord = SanitizeString(_fullWord);
        translation.Clear();
        foreach (string tr in _translation) {
            translation.Add(SanitizeString(tr));
        }
        definition = SanitizeString(_definition);
        remembered = rem;
        forgotten = forg;

        rememberedTotal = remTotal;
        forgottenTotal = forgTotal;

        fullWordScore = fwScore;
        translationScore = trScore;
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
    public bool checkAnswerTranslation(string inputTranslation) {
        foreach (string tr in this.translation) {
            if (inputTranslation == convertToCheck(tr))
                return true;
        }
        return false;
    }

    public bool checkAnswerWord (string inputTranslation) {
        string toCompare = convertToCheck (this.fullWord);
        return (toCompare == inputTranslation);
    }

    private static string convertToCheck (string s) {
        string res = "";
        int st = 0, ed = s.Length - 1;
        for(int i = 0; i < s.Length; i++) {
            if (s[i] != ' ') {
                st = i;
                break;
            }
        }
        for (int i = s.Length - 1; i >= 0; i--) {
            if (s[i] != ' ') {
                ed = i;
                break;
            }
        }
        s = s.Substring (st, ed - st + 1);

        for (int i = 0; i < s.Length; i++) {
            if (s[i] != '(') {
                res += s[i];
                continue;
            }
            int ind = -1;
            for (int j = i + 1; j < s.Length; j++) {
                if (s[j] == ')') {
                    ind = j;
                    break;
                }
            }
            if (ind != -1) {
                i = ind;
            }
        }
        return res;
    }
}
