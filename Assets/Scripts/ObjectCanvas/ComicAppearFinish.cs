using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicAppearFinish : MonoBehaviour
{
    public float timer;
    public GameObject thingToOpen;

    public GameObject thingToDestroy;
    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            thingToDestroy.SetActive(false);
            thingToOpen.SetActive(true);
            
        }
    }
}
