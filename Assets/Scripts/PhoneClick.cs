using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneClick : MonoBehaviour
{
    public GameObject phoneCanvas;
    public string subtitleMessage;
    private void OnMouseDown()
    {
        phoneCanvas.SetActive(true);
        GetComponent<AudioSource>().Play();
        SubtitleManager.Instance.ShowSubtitle(subtitleMessage);
    }
}
