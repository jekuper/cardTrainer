using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pushReminder : MonoBehaviour
{
    public Vector2 targetPosition;
    public RectTransform canvas;
    public TextMeshProUGUI txt;
    public GameObject mask;
    [Range(0.001f, 1)]public float posLerp = 0.1f;

    public void ShowReminder(string text) {
        mask.SetActive(true);
        txt.text = text;
        targetPosition = new Vector2(0, 0);
    }
    public void HideReminder() {
        mask.SetActive(false);
        targetPosition = new Vector2(0, -canvas.rect.height);
    }
    private void Start() {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -canvas.rect.height);
        targetPosition = GetComponent<RectTransform>().anchoredPosition;
    }
    private void Update() {
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
