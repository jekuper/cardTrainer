using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Globals
{
    public static List<word> dataBase = new List<word>();

    public static void AddWord(string word, string translation, string definition = "") {
        if (word == "" || translation == "")
            return;
        foreach (word item in dataBase) {
            if (item.fullWord == word)
                return;
        }
        word w = new word(word, translation, definition);
        dataBase.Add(w);

        SaveWordData();
    }

    public static int DeleteWord(string word) {
        for (int i = 0; i < dataBase.Count; i++) {
            if (dataBase[i].fullWord == word) {
                dataBase.RemoveAt(i);

                SaveWordData();

                return 0;
            }
        }
        return 1;
    }
    public static int DeleteWord(int ind) {
        if (ind >= dataBase.Count)
            return 1;
        dataBase.RemoveAt(ind);
        SaveWordData();
        return 0;
    }

    public static void LoadWordData() {
        dataBase.Clear();
        string[] lines = LoadId("wordBase");
        foreach (string line in lines) {
            dataBase.Add(new word(line));
        }
    }
    public static void SaveWordData() {
        string encrypted = "";
        foreach (word item in dataBase) {
            encrypted += item.toOneString();
            encrypted += "\n";
        }
        if (encrypted.Length > 0)
            encrypted = encrypted.Remove(encrypted.Length - 1);
        SaveId("wordBase", encrypted);
    }

    public static void SaveId(string fileId, string data) {
        string path = Application.persistentDataPath + "/" + fileId + ".save";
        File.WriteAllText(path, data);
    }
    public static string[] LoadId(string fileId) {
        string path = Application.persistentDataPath + "/" + fileId + ".save";
        if (!File.Exists(path)) {
            return new string[0];
        }
        string[] lines = File.ReadAllLines(@path);
        return lines;
    }
    
}

public static class ScaleSystem {
    public static Vector2 scalePosToCanvas(Vector2 posOnScreen, RectTransform canvas) {
        return new Vector2(posOnScreen.x / Screen.width * canvas.rect.width, posOnScreen.y / Screen.height * canvas.rect.height);
    }
}
