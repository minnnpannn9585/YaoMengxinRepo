using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBookOpen : MonoBehaviour
{
    public GameObject phoneCanvas;

    public void OpenPhoneBook()
    {
        phoneCanvas.SetActive(true);
    }
}
