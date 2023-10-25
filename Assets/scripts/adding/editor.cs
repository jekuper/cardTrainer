using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class editor : MonoBehaviour
{
    public Vector2 targetPosition;
    public Canvas canvas;
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
        rt.sizeDelta = new Vector2 (rt.sizeDelta.x, 1100 * Screen.height / canvas.scaleFactor / canvas.GetComponent<CanvasScaler> ().referenceResolution.y);
        targetPosition = new Vector2(0, rt.sizeDelta.y / 2 * -1f);
        rt.anchoredPosition = targetPosition;
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
        targetPosition = new Vector2(0, (Screen.height) / 2 / canvas.scaleFactor);
    }
    public void HideEditor(bool saveChages) {
        mask.SetActive(false);
        if (saveChages) {
            int rem = Globals.dataBase[selectedWordInd].remembered, forg = Globals.dataBase[selectedWordInd].forgotten;
            int remTotal = Globals.dataBase[selectedWordInd].rememberedTotal, forgTotal = Globals.dataBase[selectedWordInd].forgottenTotal;
            int trSc = Globals.dataBase[selectedWordInd].translationScore;
            int fwSc = Globals.dataBase[selectedWordInd].fullWordScore;
            Globals.DeleteWord(selectedWordInd);
            Globals.AddWord(new word(wordInputField.text, trManager.copy, definitionInputField.text, rem, forg, remTotal, forgTotal, trSc, fwSc), false);
            arch.UpdateWordList();
        }
        targetPosition = new Vector2 (0, rt.sizeDelta.y / 2 * -1f);
        selectedWordInd = -1;
        wordInputField.text = "";
//        translationInputField.text = "";
        definitionInputField.text = "";
    }
    void Update()
    {
        #region positionSupport
        Vector2 curPos = rt.anchoredPosition;
        if (Vector2.Distance (curPos, targetPosition) > 0.01) {
            curPos = Vector2.Lerp (curPos, targetPosition, posLerp);
            rt.anchoredPosition = curPos;
        } else {
            rt.anchoredPosition = targetPosition;
        }
        #endregion
    }
}
