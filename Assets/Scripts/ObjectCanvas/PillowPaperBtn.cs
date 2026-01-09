using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowPaperBtn : MonoBehaviour
{
    public GameObject paperShow;
    public string subtitleMessage;

    public void ShowPaper()
    {
        paperShow.SetActive(true);
        GetComponent<AudioSource>().Play();
        SubtitleManager.Instance.ShowSubtitle(subtitleMessage);
    }
}
