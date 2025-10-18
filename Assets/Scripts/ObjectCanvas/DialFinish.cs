using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialFinish : MonoBehaviour
{
    public float timer;
    public GameObject comicAnim;
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            comicAnim.SetActive(true);
        }
    }
}
