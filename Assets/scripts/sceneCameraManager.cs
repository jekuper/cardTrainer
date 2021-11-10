using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneCameraManager : MonoBehaviour
{
    public pushReminder reminder;
    public void ChangeScene(string sceneName) {
        if (sceneName == "trainMenu" && Globals.dataBase.Count < 10) {
            if (reminder != null)
                reminder.ShowReminder("you need at least 10 words to unlock trainings");
            else
                Debug.LogWarning("reminder is not set. Object name: " + name);
            return;
        }
        if (sceneName == "addingMenu" && Settings.curLang == "") {
            if (reminder != null)
                reminder.ShowReminder("you need to choose/select current language in settings");
            else
                Debug.LogWarning("reminder is not set. Object name: " + name);
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
