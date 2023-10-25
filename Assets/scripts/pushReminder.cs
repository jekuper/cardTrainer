using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class pushReminder : MonoBehaviour
{
    public Vector2 targetPosition;
    public Canvas canvas;
    public TextMeshProUGUI txt;
    public GameObject mask;
    [Range(0.001f, 1)]public float posLerp = 0.1f;

    private RectTransform rt;

    public void ShowReminder(string text) {
        mask.SetActive(true);
        txt.text = text;
        targetPosition = new Vector2 (0, (Screen.height) / 2 / canvas.scaleFactor);
    }
    public void HideReminder() {
        mask.SetActive(false);
        targetPosition = new Vector2 (0, rt.sizeDelta.y / 2 * -1f);
    }
    private void Start() {
        rt = GetComponent<RectTransform> ();
        rt.sizeDelta = new Vector2 (rt.sizeDelta.x, 1100 * Screen.height / canvas.scaleFactor / canvas.GetComponent<CanvasScaler> ().referenceResolution.y);
        targetPosition = new Vector2 (0, rt.sizeDelta.y / 2 * -1f);
        rt.anchoredPosition = targetPosition;
    }
    private void Update() {
        #region positionSupport
        Vector2 curPos = rt.anchoredPosition;
        if (Vector2.Distance(curPos, targetPosition) > 0.01) {
            curPos = Vector2.Lerp(curPos, targetPosition, posLerp);
            rt.anchoredPosition = curPos;
        } else {
            rt.anchoredPosition = targetPosition;
        }
        #endregion
    }
}
