using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowPaperAppear : MonoBehaviour
{
    public float timer;
    public GameObject pillowCanvas;
    public GameObject phonePaper;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (phonePaper != null) {
                phonePaper.SetActive(true);
            }
            
            pillowCanvas.SetActive(false);
        }
    }
}
