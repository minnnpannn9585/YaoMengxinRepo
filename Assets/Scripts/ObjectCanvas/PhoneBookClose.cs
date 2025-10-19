using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBookClose : MonoBehaviour
{

    public GameObject phoneCanvas;

    public void CloseBook()
    {
        phoneCanvas.SetActive(false);
    }
}
