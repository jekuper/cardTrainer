using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Globals
{

    public static List<word> dataBase = new List<word>();

    public static void AddWord(word w, bool resetScores = true) {
        bool isFound = false;
        for (int i = 0; i < dataBase.Count; i++) {
            if (dataBase[i].fullWord == w.fullWord) {
                foreach (string tr in w.translation) {
                    if (!dataBase[i].translation.Contains(tr)) {
                        dataBase[i].translation.Add(word.SanitizeString(w.translation[0]));
                    }
                }
                isFound = true;
                break;
            }
        }
        if (!isFound) {
            if (resetScores) {
                int mnScore = 1000000000;
                for (int i = 0; i < dataBase.Count; i++) {
                    mnScore = Mathf.Min (mnScore, dataBase[i].remembered - dataBase[i].forgotten);
                }
                if (mnScore > 0) {
                    for (int i = 0; i < dataBase.Count; i++) {
                        dataBase[i].remembered -= mnScore;
                    }
                }
            }
            dataBase.Add(w);
        }

        SaveSystem.SaveWordData(Settings.curLang);
    }

    public static int DeleteWord(string word) {
        for (int i = 0; i < dataBase.Count; i++) {
            if (dataBase[i].fullWord == word) {
                dataBase.RemoveAt(i);

                SaveSystem.SaveWordData(Settings.curLang);

                return 0;
            }
        }
        return 1;
    }
    public static int DeleteWord(int ind) {
        if (ind >= dataBase.Count)
            return 1;
        dataBase.RemoveAt(ind);
        SaveSystem.SaveWordData(Settings.curLang);
        return 0;
    }
    private static int statComp(int x, int y) {
        float cnt1 = dataBase[x].remembered - dataBase[x].forgotten;
        /*
        if (dataBase[x].forgotten + dataBase[x].remembered != 0)
            cnt1 = dataBase[x].remembered / (float)(dataBase[x].forgotten + dataBase[x].remembered);
        */
        float cnt2 = dataBase[y].remembered - dataBase[y].forgotten;
        /*
        if (dataBase[y].forgotten + dataBase[y].remembered != 0)
            cnt2 = dataBase[y].remembered / (float)(dataBase[y].forgotten + dataBase[y].remembered);
        else
            cnt2 = 0;
        */
        return cnt1.CompareTo(cnt2);
    }
    public static List<int> sortByStatistic(int cnt) {

        List<int> te = new List<int>();
        for (int i = 0; i < dataBase.Count; i++) {
            te.Add(i);
        }
        te.Sort(statComp);

        List<int> res = new List<int>();

        for (int i = 0; i < Mathf.Min(te.Count, cnt); i++) {
            res.Add(te[i]);
        }

        for (int i = 0; i < res.Count; i++) {
            int temp = res[i];
            int randomIndex = Random.Range(i, res.Count);
            res[i] = res[randomIndex];
            res[randomIndex] = temp;
        }

        return res;
    }

    
    

    
}



public static class ScaleSystem {

    public static Vector2 canvasScaleSize = new Vector2 (720, 1280);

    public static Vector2 scalePosToCanvas(Vector2 posOnScreen, RectTransform canvas) {
        return new Vector2(posOnScreen.x / Screen.width * canvas.rect.width, posOnScreen.y / Screen.height * canvas.rect.height);
    }
}
