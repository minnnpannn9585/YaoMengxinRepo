using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSubtitle : MonoBehaviour
{
    public string subtitleMessage;
    // Start is called before the first frame update
    void Start()
    {
        SubtitleManager.Instance.ShowSubtitle(subtitleMessage);
    }

    
}
