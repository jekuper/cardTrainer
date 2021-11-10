using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class editor : MonoBehaviour
{
    public Vector2 targetPosition;
    public GameObject canvas;
    [Range(0.001f, 1)] public float posLerp = 0.1f;
    public int selectedWordInd = -1;

    public TMP_InputField wordInputField;
    public TMP_InputField definitionInputField;
    public editorTranslationManager trManager;
    public statisticManager stManager;
    public GameObject mask;
    public archive arch;

    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, -canvas.GetComponent<RectTransform>().rect.height);
        targetPosition = rt.anchoredPosition;
    }

    public void ShowEditor(word w) {
        int ind = -1;
        mask.SetActive(true);
        for (int i = 0; i < Globals.dataBase.Count; i++) {
            if (Globals.dataBase[i].fullWord == w.fullWord && Globals.dataBase[i].translation == w.translation) {
                ind = i;
                break;
            }
        }
        if (ind == -1)
            return;
        selectedWordInd = ind;
        wordInputField.text = word.decrypt(Globals.dataBase[ind].fullWord);
        trManager.copy = new List<string>(w.translation);
        trManager.InitList();
        definitionInputField.text = word.decrypt(Globals.dataBase[ind].definition);
        stManager.LoadStatistic(w);
        targetPosition = new Vector2(0, 0);
    }
    public void HideEditor(bool saveChages) {
        mask.SetActive(false);
        if (saveChages) {
            int rem = Globals.dataBase[selectedWordInd].remembered, forg = Globals.dataBase[selectedWordInd].forgotten;
            Globals.DeleteWord(selectedWordInd);
            Globals.AddWord(new word(wordInputField.text, trManager.copy, definitionInputField.text, rem, forg));
            arch.UpdateWordList();
        }
        targetPosition = new Vector2(0, -canvas.GetComponent<RectTransform>().rect.height);
        selectedWordInd = -1;
        wordInputField.text = "";
//        translationInputField.text = "";
        definitionInputField.text = "";
    }
    void Update()
    {
        #region positionSupport
        Vector2 curPos = GetComponent<RectTransform>().anchoredPosition;
        if (targetPosition != curPos) {
            if (Mathf.Abs(curPos.x - targetPosition.x) < 5 && Mathf.Abs(curPos.y - targetPosition.y) < 5) {
                curPos = targetPosition;
            }
            else
                curPos = Vector2.Lerp(curPos, targetPosition, posLerp);
            GetComponent<RectTransform>().anchoredPosition = curPos;
        }
        #endregion
    }
}
