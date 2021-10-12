using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteButton : MonoBehaviour
{
    public editor ed;
    public void deleteWord() {
        Globals.DeleteWord(ed.selectedWordInd);
        ed.HideEditor(false);
    }
}
