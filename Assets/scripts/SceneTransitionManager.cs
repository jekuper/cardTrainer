using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager singleton;

    [SerializeField] private Animator animator;
    [SerializeField] private float animDuration;

    private void Start () {
        if (singleton != null) {
            Debug.LogWarning ("dublicating transition found. Deleting dublicate");
            Destroy (gameObject);
            return;
        }
        singleton = this;
    }

    public void In (string name) {
        animator.SetTrigger ("translate");
        StartCoroutine (Load (name));
    }
    private IEnumerator Load (string name) {
        yield return new WaitForSeconds (animDuration);
        SceneManager.LoadScene (name);
    }
}
