using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTransition : MonoBehaviour
{
    public int offsetX = 2, offsetY = 0;
    public RectTransform[] textRect;
    List<Vector3> pos = new List<Vector3>();

    void Start() {
        for (int i = 0; i < textRect.Length; i++) {
            pos.Add(textRect[i].localPosition);
        }
    }

    public void Down() {
        for (int i = 0; i < textRect.Length; i++) {
            textRect[i].localPosition = new Vector3(pos[i].x + (float)offsetX, pos[i].y - (float)offsetY, pos[i].z);
        }
    }

    public void Up() {
        for (int i = 0; i < textRect.Length; i++) {
            textRect[i].localPosition = pos[i];
        }
    }
}
