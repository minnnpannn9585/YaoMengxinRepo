using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneClick : MonoBehaviour
{
    public GameObject phoneCanvas;
    private void OnMouseDown()
    {
        phoneCanvas.SetActive(true);
    }
}
