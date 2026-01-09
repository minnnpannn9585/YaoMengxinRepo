using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance;
    public TMP_Text subtitleText;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowSubtitle(string message)
    {
        // Implement subtitle display logic here
        //Debug.Log($"Subtitle: {message} (Duration: {duration}s)");
        StopAllCoroutines();
        subtitleText.gameObject.SetActive(true);
        subtitleText.text = message;
        StartCoroutine(HideSubtitleAfterDelay());
    }

    IEnumerator HideSubtitleAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        subtitleText.gameObject.SetActive(false);
    }
}
