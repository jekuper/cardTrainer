using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class statisticManager : MonoBehaviour
{
    public RectTransform remember, forget;
    public TextMeshProUGUI remText, forgText, percentageText;

    private float maxHeight;
    private void Start() {
        maxHeight = remember.sizeDelta.y;
    }
    public void LoadStatistic(word w) {
        int cnt1 = w.remembered, cnt0 = w.forgotten;
        remText.text = "remembered: " + cnt1.ToString();
        forgText.text = "forgotten: " + cnt0.ToString();
        if (cnt0 != 0)
            percentageText.text = ((int)((float)cnt1 / (float)cnt0 * 100)).ToString() + "%";
        if (cnt1 >= cnt0) {
            remember.sizeDelta = new Vector2(remember.sizeDelta.x, maxHeight);
            if (cnt1 != 0)
                forget.sizeDelta = new Vector2(forget.sizeDelta.x, cnt0 * maxHeight / cnt1);
        }
        else {
            forget.sizeDelta = new Vector2(forget.sizeDelta.x, maxHeight);
            if (cnt0 != 0)
                remember.sizeDelta = new Vector2(remember.sizeDelta.x, cnt1 * maxHeight / cnt0);
        }

        if (cnt1 == 0) {
            remember.sizeDelta = new Vector2(remember.sizeDelta.x, 2);
        }
        if (cnt0 == 0) {
            percentageText.text = "--%";
            forget.sizeDelta = new Vector2(forget.sizeDelta.x, 2);
        }
    }
}
