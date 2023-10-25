using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings {
    public static string curLang = "";
    public static List<string> avLangs = new List<string>();


    public static void AddLang(string lang) {
        if (avLangs.Contains(lang))
            return;
        avLangs.Add(lang);
        if (avLangs.Count == 1)
            curLang = lang;
    }
    public static void DeleteLang(string lang) {
        if (avLangs.Count < 2)
            return;
        avLangs.Remove(lang);
        if (lang == curLang)
            curLang = avLangs[0];
    }

    
}