using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainFragSubtitle : MonoBehaviour
{
    public string subtitleMessage;
    private void OnMouseOver()
    {
        SubtitleManager.Instance.ShowSubtitle(subtitleMessage);
    }
}
