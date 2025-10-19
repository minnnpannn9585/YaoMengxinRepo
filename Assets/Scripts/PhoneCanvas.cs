using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCanvas : MonoBehaviour
{
    public GameObject mainCanvas;

    private void OnEnable()
    {
        mainCanvas.SetActive(false);
    }

    private void OnDisable()
    {
        mainCanvas.SetActive(true); 
    }

    public void ProtectBtn()
    {
        gameObject.SetActive(false);
    }

    public void DestBtn()
    {
        gameObject.SetActive(false);
    }
}
