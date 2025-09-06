using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTriggerArea : MonoBehaviour
{
    public GameObject phoneLight;
    public Collider2D phoneClick;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //turn on light
        phoneLight.SetActive(true);
        //turn on phone click
        phoneClick.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //turn off light
        phoneLight.SetActive(false);
        //turn off phone click
        phoneClick.enabled = false;
    }
}
