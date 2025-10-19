using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAfterBreak : MonoBehaviour
{
    public GameObject thingToOpen;
    public GameObject thingToClose;

    public void btnAfterBreak()
    {
        if (thingToClose != null)
        {
            //thingToClose.SetActive(false);
            Destroy(thingToClose);
        }
            
        if (thingToOpen != null)
        {
            thingToOpen.SetActive(true);
        }
            
    }
}
