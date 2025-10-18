using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    public float timer;
    public GameObject thingToActive;

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
            if (thingToActive != null)
            {
                thingToActive.SetActive(true);
            }
        }
    }
}
