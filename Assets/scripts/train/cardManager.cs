using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class cardManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject wordText;
    public GameObject translationText;
    public GameObject definitionText;
    public GameObject answerInputField;
    public bool isCardOpened = false;
    public bool isCardUsed = false;
    public bool isCardChoosed = false;
    public bool isCardInputReversed = false;
    [Range(0, 360)]public float rotSpeed = 100f;
    [Range(0.001f, 1)] public float sizeLerp = 0.1f;
    [Range(0.001f, 1)] public float posLerp = 0.1f;
    public int wordTranslationInd = -1;
    public int carryWordInd;


    public Vector3 targetAngle = new Vector3(0, 0, 0);
    public Vector2 targetPosition = new Vector2(0, 0);
    public Vector2 targetSize = new Vector2(0, 0);
    public Vector2 startPosition;


    public void ShowWordSide() {
        targetAngle = new Vector3(0, 0, 0);
        isCardOpened = false;
    }
    public void ShowTranslationSide() {
        targetAngle = new Vector3(0, 180, 0);
        isCardOpened = true;
        Globals.dataBase[carryWordInd].forgotten++;
        if (isCardInputReversed) {
            Globals.dataBase[carryWordInd].translationScore--;
        }
        else {
            Globals.dataBase[carryWordInd].fullWordScore--;
        }
    }
    public void AcceptCard() {
        Globals.dataBase[carryWordInd].remembered++;
        if (isCardInputReversed) {
            Globals.dataBase[carryWordInd].translationScore++;
        }
        else {
            Globals.dataBase[carryWordInd].fullWordScore++;
        }
    }
    public void LoadWord() {
        string front, back;
        front = word.decrypt(Globals.dataBase[carryWordInd].fullWord);
        back = word.decrypt(Globals.dataBase[carryWordInd].translationToString());
        if (isCardInputReversed) {
            back = front;
            if (wordTranslationInd == -1)
                wordTranslationInd = Random.Range(0, Globals.dataBase[carryWordInd].translation.Count - 1);
            front = Globals.dataBase[carryWordInd].translation[wordTranslationInd];
        }
        if (wordText.GetComponent<TextMeshProUGUI>().enabled) {
            wordText.GetComponent<TextMeshProUGUI>().text = front;
        }

        if (translationText.GetComponent<TextMeshProUGUI>().enabled) {
            translationText.GetComponent<TextMeshProUGUI>().text = back;
        }

        if (definitionText.GetComponent<TextMeshProUGUI>().enabled) {
            definitionText.GetComponent<TextMeshProUGUI>().text = word.decrypt(Globals.dataBase[carryWordInd].definition);
        }
        if (answerInputField.activeSelf && isCardInputReversed) {
            answerInputField.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "full word...";
        }
    }
    public void ChooseThisWord() {
        if (train.isCardSelected || isCardUsed)
            return;
        train.leaveButton.interactable = false;
        targetSize = new Vector2(244, 400);
        targetPosition = new Vector3(-360, -640, 0);
        answerInputField.SetActive(true);
        transform.SetAsLastSibling();
    }
    private void Awake() {
        startPosition = GetComponent<RectTransform>().anchoredPosition;
        targetPosition = GetComponent<RectTransform>().anchoredPosition;
        targetSize = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);
    }
    void Update()
    {
        #region sizeSupport
        Vector2 curSize = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);
        if (targetSize != curSize) {
            if (Mathf.Abs(curSize.x - targetSize.x) < 5 && Mathf.Abs(curSize.y - targetSize.y) < 5) {
                curSize = targetSize;
            } else
                curSize = Vector2.Lerp(curSize, targetSize, sizeLerp);
            GetComponent<RectTransform>().sizeDelta = curSize;
        }
        #endregion

        #region positionSupport
        Vector2 curPos = GetComponent<RectTransform>().anchoredPosition;
        if (targetPosition != curPos) {
            if (Mathf.Abs(curPos.x - targetPosition.x) < 5 && Mathf.Abs(curPos.y - targetPosition.y) < 5) {
                curPos = targetPosition;
            } else
                curPos = Vector2.Lerp(curPos, targetPosition, posLerp);
            GetComponent<RectTransform>().anchoredPosition = curPos;
        }
        #endregion

        #region rotationSupport
        if (Mathf.Abs(GetComponent<RectTransform>().eulerAngles.y - targetAngle.y) > 10) {
            Vector3 res;
            res = new Vector3(0, GetComponent<RectTransform>().eulerAngles.y + rotSpeed * Time.deltaTime, 0);
            GetComponent<RectTransform>().eulerAngles = res;
        }
        else if (GetComponent<RectTransform>().eulerAngles != targetAngle) {
            GetComponent<RectTransform>().eulerAngles = targetAngle;
        }




        if (GetComponent<RectTransform>().eulerAngles.y >= 90 && GetComponent<RectTransform>().eulerAngles.y <= 270) {
            wordText.GetComponent<TextMeshProUGUI>().enabled = false;
            answerInputField.SetActive(false);
            translationText.GetComponent<TextMeshProUGUI>().enabled = true;
            definitionText.GetComponent<TextMeshProUGUI>().enabled = true;
            translationText.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 180, 0);
            definitionText.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 180, 0);
            LoadWord();
        }
        else {
            wordText.GetComponent<TextMeshProUGUI>().enabled = true;
            translationText.GetComponent<TextMeshProUGUI>().enabled = false;
            definitionText.GetComponent<TextMeshProUGUI>().enabled = false;
            translationText.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
            definitionText.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
            LoadWord();
        }
        #endregion
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (train.isCardSelected || isCardOpened || isCardUsed)
            return;
        isCardChoosed = true;
        ChooseThisWord();
        isCardUsed = true;
        train.isCardSelected = true;
        train.chosenCard = gameObject;
    }
}
