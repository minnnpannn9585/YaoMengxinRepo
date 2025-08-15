using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject anim;

    private void OnMouseDown()
    {
        anim.SetActive(true);
    }
}
