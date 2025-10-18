using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNumCollected : MonoBehaviour
{
    public GameObject[] nums;

    public GameObject dialAnim;
    
    void Update()
    {
        if (nums[0].activeSelf && nums[1].activeSelf && nums[2].activeSelf)
        {
            dialAnim.SetActive(true);
            this.gameObject.SetActive(false);   
        }
    }
}
