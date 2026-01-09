using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitToLoadNext : MonoBehaviour
{
    public float duration;

    private void OnEnable()
    {
        StartCoroutine(LoadNextSceneAfterDelay(duration));
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
