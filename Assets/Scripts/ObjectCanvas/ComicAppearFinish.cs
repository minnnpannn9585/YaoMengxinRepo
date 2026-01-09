using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicAppearFinish : MonoBehaviour
{
    public float timer;
    public GameObject thingToOpen;

    public GameObject thingToDestroy;

    public string subtitleMessage;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (thingToDestroy != null)
            {
                thingToDestroy.GetComponent<MeshRenderer>().enabled = false;
                SubtitleManager.Instance.ShowSubtitle(subtitleMessage);
            }
                
            if (thingToOpen != null)
                thingToOpen.SetActive(true);
            
        }
    }
}
