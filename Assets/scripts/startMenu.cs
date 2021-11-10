using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class startMenu : MonoBehaviour
{
    public TextMeshProUGUI versionText;
    void Start()
    {
        versionText.text = "@Jekuper, "+Application.productName+", ver. "+ Application.version;
        SaveSystem.LoadSettingsData();
        SaveSystem.LoadWordData(Settings.curLang);
    }
}
