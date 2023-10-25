using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public static class SaveSystem
{
    
    #region base_system
    public static void SaveId(string fileId, string data) {
        string path = Application.persistentDataPath + "/" + fileId + ".save";
        data = "version="+Application.version+"\n"+data;
        File.WriteAllText(path, data);
    }
    public static string LoadFileVersion(string fileId) {
        string path = Application.persistentDataPath + "/" + fileId + ".save";
        if (!File.Exists(path)) {
            return null;
        }
        string res = File.ReadAllLines(@path).First();
        if (res.Length > 8 && res.Substring(0, 8) == "version=") {
            return res.Substring(8);
        }
        return null;
    }
    public static string[] LoadId(string fileId) {
        string path = Application.persistentDataPath + "/" + fileId + ".save";
        if (!File.Exists(path)) {
            return new string[0];
        }
        string[] lines = File.ReadAllLines(@path);
        if (lines.Length > 0 && lines[0].Length > 8 && lines[0].Substring(0, 8) == "version=") {
            List<string> tl = new List<string>(lines);
            tl.RemoveAt(0);
            lines = tl.ToArray();
        }
        return lines;
    }

    private static string SearchLoaderVersion(Version version, Dictionary<string, Action<string[]>> loaders) {
        string prev = null;
        string resVersion = null;
        foreach (KeyValuePair<string, Action<string[]>> entry in loaders) {
            Version cur = new Version(entry.Key);
            if (version == null) {
                resVersion = entry.Key;
                break;
            }
            resVersion = entry.Key;
            if (version.CompareTo(cur) < 0) {
                if (prev == null) {
                    resVersion = entry.Key;
                }
                else {
                    resVersion = prev;
                }
                break;
            }
            if (version.CompareTo(cur) == 0) {
                resVersion = entry.Key;
                break;
            }
            prev = entry.Key;
        }
        return resVersion;
    }

    #endregion

    #region word_system
    private static Dictionary<string, Action<string[]>> wordLoaders = new Dictionary<string, Action<string[]>>() {
        {"2.1.3", (string[] s) => WLoader2_1_3(s)},
        {"2.2.1", (string[] s) => WLoader2_2_1(s)},
    };

    public static void LoadWordData(string language) {
        Globals.dataBase.Clear();
        if (language == "")
            return;
        string[] lines = LoadId(language);
        string version = LoadFileVersion(language);
        string resVersion = SearchLoaderVersion((version != null ? new Version(version) : null), wordLoaders);

        wordLoaders[resVersion](lines);
    }
    public static void SaveWordData(string language) {
        string encrypted = JsonConvert.SerializeObject(Globals.dataBase);
        SaveId(language, encrypted);
    }
    
    #region load handlers
    private static void WLoader2_1_3(string[] lines) {
        foreach (var oneString in lines) {
            word w = new word();
            string[] dt = oneString.Split('-');
            w.fullWord = dt[0];
            w.translation = new List<string>(dt[1].Split('|'));
            w.translation.RemoveAt(w.translation.Count - 1);
            w.definition = dt[2];

            foreach (string item in dt[3].Split('|')) {
                if (item != "") {
                    int v = int.Parse(item);
                    if (v == 1)
                        w.remembered++;
                    else
                        w.forgotten++;
                }
            }
            var time = DateTime.ParseExact(dt[4], "G", null);//no longer exist in 2.2.1 or above
            Globals.dataBase.Add(w);
        }
    }
    private static void WLoader2_2_1(string[] lines) {
        string file = "";
        foreach (var line in lines) {
            file += line + "\n";
        }
        Globals.dataBase = JsonConvert.DeserializeObject<List<word>>(file);
    }
    #endregion

    #endregion

    #region settings_system

    private static Dictionary<string, Action<string[]>> SettingsLoaders = new Dictionary<string, Action<string[]>>() {
        {"2.1.3", (string[] s) => SLoader2_1_3(s)},
    };

    public static void LoadSettingsData() {
        string[] lines = LoadId("settings");
        string version = LoadFileVersion("settings");
        string resVersion = SearchLoaderVersion((version != null ? new Version(version) : null), SettingsLoaders);
        SettingsLoaders[resVersion](lines);
    }
    public static void SaveSettingsData() {
        string res = "";
        res += Settings.curLang;
        res += "\n";
        foreach (string lang in Settings.avLangs) {
            res += lang;
            res += "|";
        }
        SaveId("settings", res);
    }

    #region load hanlers
    private static void SLoader2_1_3(string [] lines) {
        if (lines.Length > 0) {
            Settings.curLang = lines[0];
        }
        if (lines.Length > 1) {
            Settings.avLangs = new List<string>(lines[1].Split('|'));
            Settings.avLangs.RemoveAt(Settings.avLangs.Count - 1);
        }
    }
    #endregion

    #endregion

    public static List<trainRecord> LoadTrainStatistics () {
        string[] lines = LoadId ("trainStats.save");

        string file = "";
        foreach (var line in lines) {
            file += line + "\n";
        }
        if (file == "") {
            return new List<trainRecord> ();
        }
        return JsonConvert.DeserializeObject<List<trainRecord>> (file);
    }
    public static void UpdateTrainStatistics (trainRecord newStats) {
        List<trainRecord> l = LoadTrainStatistics ();
        l.Add (newStats);
        SaveTrainStatistics (l);
    }
    public static void SaveTrainStatistics (List<trainRecord> l) {
        string encrypted = JsonConvert.SerializeObject (l);
        SaveId ("trainStats.save", encrypted);
    }
}
