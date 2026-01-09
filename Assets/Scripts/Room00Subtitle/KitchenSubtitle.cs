using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenSubtitle : MonoBehaviour
{
    public string subtitleMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SubtitleManager.Instance.ShowSubtitle(subtitleMessage);
        }
    }
}
